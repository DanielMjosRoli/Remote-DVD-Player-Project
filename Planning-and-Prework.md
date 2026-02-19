# Architecture Planning Guide

## Disc Management & iSCSI Orchestrator (.NET + React)

------------------------------------------------------------------------

# 1. Start With System Boundaries (C4 Level 1 & 2)

## Level 1 -- System Context

### External Actors

-   Admin (Web UI)
-   Companion App (Client Device)
-   Linux Host (runs iSCSI)
-   Database

### System Responsibility

**Disc Management & iSCSI Orchestrator**

### Context Diagram (Conceptual)

Admin → Web UI → Backend API → Linux iSCSI\
Companion App → Backend API

------------------------------------------------------------------------

# 2. Define Containers (C4 Level 2)

Containers:

-   React Frontend
-   ASP.NET Core API
    -   Application Layer
    -   Domain Layer
    -   Infrastructure Layer
-   PostgreSQL Database
-   Linux iSCSI Service

------------------------------------------------------------------------

# 3. Define Core Capabilities (Use Cases)

## Image Management

-   Upload image
-   Validate image
-   Store metadata
-   Delete image

## Target Management

-   Create iSCSI target
-   Delete target
-   Enforce read-only
-   Generate IQN

## Client Management

-   Register client
-   Track sessions
-   Enforce ACL

------------------------------------------------------------------------

# 4. Define Domain Aggregates

Entities with identity and lifecycle:

-   DiscImage
-   IscsiTarget
-   ClientDevice
-   ClientSession

Relationships:

-   DiscImage 1 → N IscsiTarget\
-   ClientDevice 1 → N ClientSession

------------------------------------------------------------------------

# 5. Domain Class Sketch

## DiscImage

-   Id
-   Name
-   FilePath
-   Sha256
-   UploadedAt
-   Validate()

## IscsiTarget

-   Id
-   Iqn
-   ImageId
-   ReadOnly
-   Status
-   Activate()
-   Deactivate()

Keep domain pure --- no EF or framework annotations.

------------------------------------------------------------------------

# 6. Define Abstractions (Interfaces)

External dependencies should be abstracted:

-   IImageRepository
-   ITargetRepository
-   IIscsiService
-   IFileStorageService
-   IShellExecutor

------------------------------------------------------------------------

# 7. Sequence Flow Example -- Create Target

Controller\
→ CreateTargetHandler\
→ IImageRepository\
→ IIscsiService\
→ ITargetRepository

This clarifies responsibilities and prevents circular dependencies.

------------------------------------------------------------------------

# 8. Early Threat Modeling

## Trust Boundaries

Internet\
→ API Layer (JWT validation)\
→ Application Layer\
→ Infrastructure Layer (Shell execution)\
→ Linux Host

Security Questions: - Can users mount arbitrary files? - Can privileges
escalate? - Can shell commands be injected? - Can read-only enforcement
be bypassed?

------------------------------------------------------------------------

# 9. Folder Structure

src/ - Server.Domain - Server.Application - Server.Infrastructure -
Server.Api

Dependency Rule:

Domain ← Application ← Infrastructure ← Api

Never reverse this direction.

------------------------------------------------------------------------

# 10. Architecture Notes Document

Create an `ARCHITECTURE.md` containing:

-   System purpose
-   High-level diagram
-   Layer responsibilities
-   Dependency rules
-   Security assumptions

------------------------------------------------------------------------

# 11. Visualization Tools

-   Excalidraw (fast and flexible)
-   Structurizr (C4 modeling)
-   PlantUML (text-based diagrams)
-   Draw.io

------------------------------------------------------------------------

# 12. Advanced Validation Trick

Before implementing real infrastructure, create:

-   FakeIscsiService
-   InMemoryImageRepository
-   FakeFileStorage

If your architecture supports these cleanly, your boundaries are
correct.

------------------------------------------------------------------------

# Final Principle

Do not start with classes.\
Start with responsibilities and boundaries.

Architecture is about separation of concerns --- not objects.
