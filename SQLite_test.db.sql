BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Cars" (
	"ID"	INTEGER,
	"Manufacturer"	TEXT,
	"Model"	TEXT,
	"year"	INTEGER,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "TEST" (
	"ID"	INTEGER,
	"Car_ID"	INTEGER,
	"IsPassed"	INTEGER,
	"Date"	datetime,
	FOREIGN KEY("Car_ID") REFERENCES "Cars"("ID"),
	PRIMARY KEY("ID" AUTOINCREMENT)
);
COMMIT;
