# 🔗 SimplifyLink

> A production-style URL shortening and analytics service built with ASP.NET Core, Entity Framework Core, SQLite, and xUnit.

SimplifyLink generates unique short URLs and tracks detailed analytics for every click — including timestamp, IP address, and User-Agent metadata.

This project demonstrates backend engineering fundamentals such as REST API design, database indexing, async programming, dependency injection, caching readiness, and unit testing.

---

🚀 Features

- Generate unique 6-character short codes (GUID-based)
- Redirect short URLs to original URLs
- Track click count
- Store detailed click metadata:
  - Timestamp (UTC)
  - IP address
  - User Agent
- Swagger API documentation
- SQLite database persistence
- Unique index on `ShortCode`
- Unit testing with xUnit (13 passing tests)
- Clean layered architecture
- Async + dependency injection best practices

---

🛠 Tech Stack

- ASP.NET Core (.NET 9)
- Entity Framework Core
- SQLite
- xUnit
- Swagger / OpenAPI
- MemoryCache (extensible for scaling)

---

📌 API Endpoints

🔹 Create Short Link

**POST** `/api/links`

**Request:**
```json
{
  "originalUrl": "http://google.com"
}
```

**Response:**
```json
{
  "id": 1,
  "originalUrl": "http://google.com",
  "shortCode": "abc123",
  "shortUrl": "http://localhost:5292/abc123"
}
```

---

🔹 Redirect

**GET** `/{shortCode}`

Example:
```
http://localhost:5292/abc123
```

Redirects to the original URL and records click metadata.

---

🔹 Analytics

**GET** `/api/links/{id}/analytics`

Returns:
- Click count
- Total click events
- Last 7-day statistics

---

🗄 Database

SQLite database file:
```
simplifylink.db
```

Tables:
- `ShortLinks`
- `ClickEvents`

Unique Index:
```
IX_ShortLinks_ShortCode
```

---

🧪 Running Tests

```bash
dotnet test
```

Includes:
- URL validation tests
- ShortCode uniqueness tests
- Service logic tests

All tests currently passing.

---

▶️ How to Run the Project

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Then open:
```
https://localhost:5292/swagger
```

---

💡 Engineering Concepts Demonstrated

- RESTful API architecture
- Database normalization & indexing
- Async programming patterns
- Dependency injection
- Unit testing & separation of concerns
- Clean backend design
- Scalability considerations
- Production-style development practices

---

👨‍💻 Author

Mohammed Akrama  
Bachelor of Information Technology (Class of 2026)  
Aspiring Software Development Engineer
