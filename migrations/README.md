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