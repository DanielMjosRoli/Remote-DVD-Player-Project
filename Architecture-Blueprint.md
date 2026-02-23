# Architecture Blueprint

## Disc Management & iSCSI Orchestrator (.NET + React)

------------------------------------------------------------------------

# 1. C4 Model -- Level 1: System Context

``` plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Context.puml

Person(admin, "Admin", "Manages disc images and targets via Web UI")
Person(companion, "Companion App", "Mounts iSCSI targets")
System(system, "Disc Management & iSCSI Orchestrator", "Manages ISO images and iSCSI targets")
System_Ext(linux, "Linux Host (LIO)", "Provides iSCSI target service")
System_Ext(db, "PostgreSQL", "Stores metadata")

Rel(admin, system, "Uses via HTTPS")
Rel(companion, system, "Authenticates & requests targets")
Rel(system, linux, "Configures targets")
Rel(system, db, "Reads/Writes metadata")

@enduml
```
<img width="429" height="544" alt="image" src="https://github.com/user-attachments/assets/7bdc6a13-292b-41c1-951e-b745b8e942fc" />

------------------------------------------------------------------------

# 2. C4 Model -- Level 2: Container Diagram

``` plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

Person(admin, "Admin")
Person(companion, "Companion App")

System_Boundary(system, "Disc Management System") {
    Container(web, "React Frontend", "React + TypeScript", "Admin UI")
    Container(api, "ASP.NET Core API", ".NET 8", "Application + Domain orchestration")
    ContainerDb(db, "PostgreSQL", "Database", "Stores images, targets, clients")
}

System_Ext(linux, "Linux LIO Service", "iSCSI target service")

Rel(admin, web, "Uses")
Rel(web, api, "HTTPS / JWT")
Rel(companion, api, "HTTPS / JWT")
Rel(api, db, "EF Core")
Rel(api, linux, "Shell / Abstraction Layer")

@enduml
```
<img width="450" height="764" alt="image" src="https://github.com/user-attachments/assets/f518bb6d-1a3a-4b74-8865-cd37558d6c81" />

------------------------------------------------------------------------

# 3. C4 Model -- Level 3: Component Diagram (Backend)

``` plantuml
@startuml
!include https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Component.puml

Container(api, "ASP.NET Core API")

Component(controller, "Controllers", "API Layer")
Component(application, "Application Layer", "Use cases / Handlers")
Component(domain, "Domain Layer", "Entities & business rules")
Component(infra, "Infrastructure Layer", "LIO + File system + DB adapters")

Rel(controller, application, "Calls")
Rel(application, domain, "Uses")
Rel(application, infra, "Uses via interfaces")

@enduml
```
<img width="501" height="375" alt="image" src="https://github.com/user-attachments/assets/6c993299-3276-466d-9468-b12216491c20" />

------------------------------------------------------------------------

# 4. DB Model
``` planuml
@startuml
hide circle
skinparam linetype ortho

entity User {
  +Id : UUID <<PK>>
  --
  Username : string <<UNIQUE>>
  Email : string
  PasswordHash : string
  Role : string
  CreatedAt : datetime
  LastLoginAt : datetime
}

entity Movie {
  +Id : UUID <<PK>>
  --
  Title : string
  OriginalTitle : string
  Description : text
  ReleaseYear : int
  DurationMinutes : int
  AgeRating : string
  PosterPath : string
  CreatedAt : datetime
  UpdatedAt : datetime
}

entity MediaFile {
  +Id : UUID <<PK>>
  --
  MovieId : UUID <<FK>>
  StorageVolumeId : UUID <<FK>>
  FilePath : string
  FileSizeBytes : long
  Checksum : string
  ContainerFormat : string
  Resolution : string
  AudioFormat : string
  SubtitleLanguages : string
  CreatedAt : datetime
}

entity StorageVolume {
  +Id : UUID <<PK>>
  --
  Name : string
  MountPath : string
  Type : string
  CapacityBytes : long
  AvailableBytes : long
  CreatedAt : datetime
}

entity Genre {
  +Id : UUID <<PK>>
  --
  Name : string <<UNIQUE>>
}

entity MovieGenre {
  +MovieId : UUID <<FK>>
  +GenreId : UUID <<FK>>
}

entity Collection {
  +Id : UUID <<PK>>
  --
  Name : string
  Description : text
}

entity CollectionMovie {
  +CollectionId : UUID <<FK>>
  +MovieId : UUID <<FK>>
}

entity WatchHistory {
  +UserId : UUID <<FK>>
  +MovieId : UUID <<FK>>
  --
  LastPositionSeconds : int
  Completed : boolean
  LastWatchedAt : datetime
}

entity Rating {
  +UserId : UUID <<FK>>
  +MovieId : UUID <<FK>>
  --
  RatingValue : int
  RatedAt : datetime
}

' =========================
' Relationships
' =========================

Movie ||--o{ MediaFile
StorageVolume ||--o{ MediaFile

Movie ||--o{ MovieGenre
Genre ||--o{ MovieGenre

Movie ||--o{ CollectionMovie
Collection ||--o{ CollectionMovie

User ||--o{ WatchHistory
Movie ||--o{ WatchHistory

User ||--o{ Rating
Movie ||--o{ Rating

@enduml
```
<img width="1081" height="499" alt="image" src="https://github.com/user-attachments/assets/7aa3f453-8d71-42ea-8801-a7a19e4151ed" />


------------------------------------------------------------------------

# Architectural Principles

-   Clean Architecture
-   Dependency Rule: Domain ← Application ← Infrastructure ← API
-   iSCSI behind abstraction (IIscsiService)
-   No shell access outside Infrastructure
-   Strict separation of concerns
