START TRANSACTION;

ALTER TABLE "Issues" ADD "Priority" integer NOT NULL DEFAULT 0;

CREATE INDEX "IX_Issues_Priority" ON "Issues" ("Priority");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220511013119_AddIssuePriority', '6.0.4');

COMMIT;
