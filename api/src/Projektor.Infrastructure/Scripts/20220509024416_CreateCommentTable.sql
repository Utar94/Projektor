﻿START TRANSACTION;

CREATE TABLE "Comments" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "IssueId" integer NOT NULL,
    "Text" text NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "CreatedById" uuid NOT NULL,
    "Deleted" boolean NOT NULL,
    "DeletedAt" timestamp with time zone NULL,
    "DeletedById" uuid NULL,
    "UpdatedAt" timestamp with time zone NULL,
    "UpdatedById" uuid NULL,
    "Uuid" uuid NOT NULL,
    "Version" integer NOT NULL,
    CONSTRAINT "PK_Comments" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Comments_Issues_IssueId" FOREIGN KEY ("IssueId") REFERENCES "Issues" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Comments_IssueId" ON "Comments" ("IssueId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220509024702_CreateCommentTable', '6.0.4');

COMMIT;