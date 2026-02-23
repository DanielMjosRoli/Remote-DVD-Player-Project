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

``` SQL
BEGIN;

-- ========================================
-- Enable UUID support
-- ========================================
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- ========================================
-- USERS
-- ========================================
CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    username TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    role TEXT NOT NULL DEFAULT 'User',
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    last_login_at TIMESTAMP
);

-- ========================================
-- MOVIES
-- ========================================
CREATE TABLE movies (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title TEXT NOT NULL,
    original_title TEXT,
    description TEXT,
    release_year INT,
    duration_minutes INT,
    age_rating TEXT,
    poster_path TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE INDEX idx_movies_title ON movies(title);

-- ========================================
-- STORAGE VOLUMES
-- ========================================
CREATE TABLE storage_volumes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    mount_path TEXT NOT NULL,
    type TEXT NOT NULL, -- Local, iSCSI, NAS
    capacity_bytes BIGINT,
    available_bytes BIGINT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

-- ========================================
-- GENRES
-- ========================================
CREATE TABLE genres (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL UNIQUE
);

-- ========================================
-- COLLECTIONS
-- ========================================
CREATE TABLE collections (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    description TEXT
);

-- ========================================
-- MEDIA FILES
-- ========================================
CREATE TABLE media_files (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    movie_id UUID NOT NULL,
    storage_volume_id UUID NOT NULL,
    file_path TEXT NOT NULL,
    file_size_bytes BIGINT NOT NULL,
    checksum TEXT,
    container_format TEXT,
    resolution TEXT,
    audio_format TEXT,
    subtitle_languages TEXT,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),

    CONSTRAINT fk_media_movie
        FOREIGN KEY(movie_id)
        REFERENCES movies(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_media_storage
        FOREIGN KEY(storage_volume_id)
        REFERENCES storage_volumes(id)
        ON DELETE RESTRICT
);

CREATE INDEX idx_media_movie ON media_files(movie_id);
CREATE INDEX idx_media_checksum ON media_files(checksum);
CREATE INDEX idx_media_storage_volume ON media_files(storage_volume_id);

-- ========================================
-- MOVIE <-> GENRE (M:N)
-- ========================================
CREATE TABLE movie_genres (
    movie_id UUID NOT NULL,
    genre_id UUID NOT NULL,

    PRIMARY KEY (movie_id, genre_id),

    CONSTRAINT fk_mg_movie
        FOREIGN KEY(movie_id)
        REFERENCES movies(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_mg_genre
        FOREIGN KEY(genre_id)
        REFERENCES genres(id)
        ON DELETE CASCADE
);

-- ========================================
-- COLLECTION <-> MOVIE (M:N)
-- ========================================
CREATE TABLE collection_movies (
    collection_id UUID NOT NULL,
    movie_id UUID NOT NULL,

    PRIMARY KEY (collection_id, movie_id),

    CONSTRAINT fk_cm_collection
        FOREIGN KEY(collection_id)
        REFERENCES collections(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_cm_movie
        FOREIGN KEY(movie_id)
        REFERENCES movies(id)
        ON DELETE CASCADE
);

-- ========================================
-- WATCH HISTORY
-- ========================================
CREATE TABLE watch_history (
    user_id UUID NOT NULL,
    movie_id UUID NOT NULL,
    last_position_seconds INT NOT NULL DEFAULT 0,
    completed BOOLEAN NOT NULL DEFAULT FALSE,
    last_watched_at TIMESTAMP NOT NULL DEFAULT NOW(),

    PRIMARY KEY (user_id, movie_id),

    CONSTRAINT fk_wh_user
        FOREIGN KEY(user_id)
        REFERENCES users(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_wh_movie
        FOREIGN KEY(movie_id)
        REFERENCES movies(id)
        ON DELETE CASCADE
);

CREATE INDEX idx_watchhistory_user ON watch_history(user_id);

-- ========================================
-- RATINGS
-- ========================================
CREATE TABLE ratings (
    user_id UUID NOT NULL,
    movie_id UUID NOT NULL,
    rating_value INT NOT NULL CHECK (rating_value BETWEEN 1 AND 5),
    rated_at TIMESTAMP NOT NULL DEFAULT NOW(),

    PRIMARY KEY (user_id, movie_id),

    CONSTRAINT fk_rating_user
        FOREIGN KEY(user_id)
        REFERENCES users(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_rating_movie
        FOREIGN KEY(movie_id)
        REFERENCES movies(id)
        ON DELETE CASCADE
);

CREATE INDEX idx_ratings_movie ON ratings(movie_id);

COMMIT;
```


------------------------------------------------------------------------

# Architectural Principles

-   Clean Architecture
-   Dependency Rule: Domain ← Application ← Infrastructure ← API
-   iSCSI behind abstraction (IIscsiService)
-   No shell access outside Infrastructure
-   Strict separation of concerns
