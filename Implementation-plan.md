# Implementation Plan & Milestones

## Disc Management & iSCSI Orchestrator

------------------------------------------------------------------------

# Phase 1 -- Foundation (Week 1)

## Goals

-   Create solution structure
-   Define Domain entities
-   Define interfaces (IIscsiService, Repositories)
-   Setup PostgreSQL & EF Core

## Deliverables

-   Compiling solution
-   Domain unit tests passing
-   Empty API skeleton

------------------------------------------------------------------------

# Phase 2 -- Image Management (Week 2)

## Goals

-   Upload ISO files
-   SHA256 validation
-   Metadata persistence
-   File storage abstraction

## Deliverables

-   Working upload endpoint
-   Validation tests
-   File storage tests

------------------------------------------------------------------------

# Phase 3 -- iSCSI Abstraction (Week 3)

## Goals

-   Implement IIscsiService
-   Implement LioIscsiService
-   Secure shell execution wrapper
-   Generate IQNs

## Deliverables

-   Create/Delete target via API
-   Integration test against real LIO
-   Read-only enforcement verified

------------------------------------------------------------------------

# Phase 4 -- Companion App Integration (Week 4)

## Goals

-   JWT authentication
-   Target discovery endpoint
-   Session tracking
-   Heartbeat mechanism

## Deliverables

-   Companion app mount flow works
-   Session tracking in DB
-   Basic audit logging

------------------------------------------------------------------------

# Phase 5 -- Security Hardening (Week 5)

## Goals

-   CHAP authentication
-   ACL enforcement
-   Role-based authorization
-   Shell command sanitization
-   Threat model review

## Deliverables

-   Security test report
-   All endpoints protected
-   No privilege escalation paths

------------------------------------------------------------------------

# Phase 6 -- Performance & Stability (Week 6)

## Goals

-   Load testing (k6)
-   Block performance testing (fio)
-   Logging & monitoring
-   Error handling refinement

## Deliverables

-   Performance metrics documented
-   No critical bottlenecks
-   Production-ready configuration

------------------------------------------------------------------------

# Optional Advanced Enhancements

-   CQRS + MediatR
-   Domain Events
-   Remote attestation
-   Structured logging (Serilog)
-   Observability (OpenTelemetry)

------------------------------------------------------------------------

# Final Milestone

✔ Clean architecture maintained\
✔ All tests passing\
✔ Security review complete\
✔ Documentation finalized\
✔ Deployable Docker setup
