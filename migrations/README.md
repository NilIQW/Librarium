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
- Populates existing null phone numbers with a temporary empty value ("")
## API Impact

- GET /api/members now guarantees all members have a PhoneNumber
- Existing clients that ignore this field continue to function

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

# V004 — AddBookRetirement Support

## Description
- Introduced an `IsRetired` flag on the `Books` table to support retiring books from the catalogue without deleting historical loan data.

## Type of Change
- Additive (non-breaking)

## API Impact
- No existing endpoints were removed or modified.
- `GET /api/books` now excludes retired books from its results.
- Loan endpoints continue to return book information for historical loans, including retired books.
- Referential integrity is preserved.

## Deployment Notes
- The migration can be safely applied before redeploying the application because the new column has a default value of `FALSE`.
- During the window between migration and redeployment, the old application continues to function normally.
- After redeployment, the service layer enforces the retirement rule and prevents new loans from being created for retired books.

## Decisions and Tradeoffs
- A developer proposed adding an `IsDeleted` flag and filtering books directly at query level.
- This approach was rejected because it risks breaking loan responses if navigation properties are filtered globally, potentially resulting in `null` book references.
- Instead, an explicit `IsRetired` flag was introduced to reflect the correct business meaning.
- Filtering is applied only in catalogue queries, while historical loan data remains intact.
- The rule preventing new loans for retired books is enforced in the service layer to ensure business consistency.

## V006 — Replace ISBN Column with Correct String Type

**Description**  
Replaced old invalid integer `ISBN` column with a new string column `IsbnText` to store proper ISBNs with hyphens and leading zeros.

**Type of change**  
Requires coordination

**API impact**
- `GET /api/books` returns `isbn` from the new string column.
- Previous ISBNs are invalid and appear as `null` in API responses.
- No endpoints removed or renamed; clients reading ISBN see safe string values.

**Deployment notes**  
Migration can be applied before redeployment. Old integer column kept temporarily to avoid breaking dependent systems. After deployment, new column used in API and services.

**Decisions and tradeoffs**
- Cannot recover old truncated ISBNs; new column required.
- Avoided in-place type conversion to prevent data corruption.
- Gradual migration allows safe transition for database and clients.
- Old column can be dropped later once all clients are updated.