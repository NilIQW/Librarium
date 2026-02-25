# Database Migrations

---

# V001 — Initial Schema

## Type of Change
- Additive (non-breaking)

## Description
- Creates initial database schema.
- Adds tables:
  - Book
  - Member
  - Loan

## API Impact
- None
- First live version of the system

## Deployment Notes
- Must be applied before first deployment

## Decisions and Tradeoffs
- Loan contains foreign keys to Book and Member
- ReturnDate is nullable to allow open loans

---

# V002 — Add Authors

## Type of Change
- Additive (non-breaking)

## Description
- Adds Authors table
- Adds BookAuthors join table
- Establishes many-to-many relationship between Book and Author
- Books may have multiple authors

## API Impact
- GET /api/books now includes:
  - Authors (array)
- Existing books without authors return:
  - Authors: []

## Deployment Notes
- Existing books remain valid
- Relationship is optional
- No placeholder authors inserted

## Decisions and Tradeoffs
- Many-to-many chosen for flexibility
- Relationship optional to avoid breaking existing data
- Returning empty array preserves backward compatibility
- Placeholder authors can be introduced later if required

---

# V003a — AddPhoneNullable

## Type of Change
- Additive (non-breaking)

## Description
- Adds `PhoneNumber` column to `Members` table as **nullable** to avoid breaking existing data.

## API Impact
- GET /api/members now includes `PhoneNumber`, but existing rows may have null.

## Deployment Notes
- Safe to apply on existing database
- Existing members remain valid
- Column is nullable to prevent migration failure

## Decisions and Tradeoffs
- Added as nullable first to safely introduce the column
- Ensures migration succeeds without requiring immediate data for all rows
- Allows population of phone numbers before making it mandatory

---

# V003b — RequirePhoneNumber

## Type of Change
- Additive (potentially breaking)

## Description
- Alters `PhoneNumber` column to be **required** (non-nullable)
- Populates existing null phone numbers with default value:

## API Impact

-GET /api/members now guarantees all members have a PhoneNumber
-Existing clients that ignore this field continue to function

## Deployment Notes

- Populate data for existing members before enforcing non-null constraint
- Migration must be applied before deploying code that expects PhoneNumber to be mandatory

## Decisions and Tradeoffs

- Default placeholder ensures migration success without data loss
- Makes future member registration and API contracts consistent
- Allows safe enforcement of non-nullability

# V003c — UniqueEmail

## Type of Change
- Additive (potentially breaking)

## Description

- Enforces unique constraint on Email column
- Checks for existing duplicates
- Assigns temporary emails to duplicates to resolve conflicts

## API Impact

- GET /api/members continues to function
- Future inserts and updates will fail if they violate unique email

## Deployment Notes

- Ensure duplicates are resolved before applying migration
- Temporary emails allow migration to succeed without deleting data

## Decisions and Tradeoffs

- Manual or scripted handling of duplicates preserves data integrity
- Temporary emails are placeholders until real emails are confirmed or corrected
- Enforcing uniqueness prevents operational issues during login