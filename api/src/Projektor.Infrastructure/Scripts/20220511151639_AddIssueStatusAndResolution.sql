START TRANSACTION;

ALTER TABLE "Issues" ADD "ClosedAt" timestamp with time zone NULL;

ALTER TABLE "Issues" ADD "ClosedById" uuid NULL;

ALTER TABLE "Issues" ADD "Resolution" integer NOT NULL DEFAULT 0;

CREATE INDEX "IX_Issues_ClosedAt" ON "Issues" ("ClosedAt");

CREATE INDEX "IX_Issues_ClosedById" ON "Issues" ("ClosedById");

CREATE INDEX "IX_Issues_Resolution" ON "Issues" ("Resolution");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220511151639_AddIssueStatusAndResolution', '6.0.4');

COMMIT;
