# 🔗 SimplifyLink

A production-style URL shortening service built with ASP.NET Core, Entity Framework Core, SQLite, and xUnit.

SimplifyLink generates unique short URLs and tracks detailed analytics for every click — including timestamp, IP address, and User-Agent metadata.

This project demonstrates backend engineering, REST API design, database modeling, indexing, caching readiness, and unit testing.

---

## 🚀 Features

- ✅ Generate unique 6-character short codes (GUID-based)
- ✅ Redirect short URLs to original URLs
- ✅ Track click count
- ✅ Store click metadata:
  - Timestamp (UTC)
  - IP address
  - User Agent
- ✅ Swagger API documentation
- ✅ SQLite database
- ✅ Unique index on ShortCode
- ✅ Unit testing with xUnit (13 passing tests)
- ✅ RESTful API design
- ✅ Clean layered architecture

---

## 🛠 Tech Stack

- ASP.NET Core (.NET 9)
- Entity Framework Core
- SQLite
- xUnit
- Swagger / OpenAPI
- MemoryCache (extensible for scaling)

---

## 📌 API Endpoints

### Create Short Link

POST /api/links:

Request:
```json
{
  "originalUrl": "http://google.com"
}

Response:

{
  "id": 1,
  "originalUrl": "http://google.com",
  "shortCode": "abc123",
  "shortUrl": "http://localhost:5292/abc123"
}

Redirect
GET /{shortCode}


Example:

http://localhost:5292/abc123


Redirects to original URL and records click metadata.

Analytics
GET /api/links/{id}/analytics


Returns:

Click count

Total events

Last 7-day statistics

🗄 Database

SQLite file:

simplifylink.db


Tables:

ShortLinks

ClickEvents

Unique Index:

IX_ShortLinks_ShortCode

🧪 Unit Testing

Run tests:

dotnet test


Includes:

URL validation tests

ShortCode uniqueness tests

Service logic tests

All tests passing.

💡 What This Project Demonstrates

Backend API architecture

Database normalization & indexing

Async programming

Dependency injection

Unit testing

Clean separation of concerns

Scalability considerations

Real-world production patterns

👨‍💻 Author

Mohammed Akrama
Bachelor of Information Technology (Class of 2026)
Aspiring Software Development Engineer