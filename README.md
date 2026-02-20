# 🔗 SimplifyLink

> A production-style URL shortening and analytics service built with ASP.NET Core, Entity Framework Core, SQLite, Docker, and xUnit.

SimplifyLink generates unique short URLs and tracks detailed analytics for every click — including timestamp, IP address, and User-Agent metadata.

This project demonstrates backend engineering fundamentals such as REST API design, database indexing, async programming, dependency injection, caching, Docker containerization, and cloud deployment.

---

## 🌐 Live Demo

🔹 Swagger API:  
https://simplifylink-api.onrender.com/swagger/index.html

🔹 Example Working Short Link:  
https://simplifylink-api.onrender.com/521e23

---

## 🚀 Features

- Generate unique 6-character short codes (GUID-based)
- Public short link redirection
- Track click count
- Store detailed click metadata:
  - Timestamp (UTC)
  - IP address
  - User Agent
- Real-time analytics (last 7 days)
- Swagger API documentation
- SQLite database persistence
- Auto database migration on production startup
- Unique index on `ShortCode`
- Unit testing with xUnit (13 passing tests)
- Clean layered architecture
- Async + dependency injection best practices
- Dockerized deployment (Render)

---

## 🛠 Tech Stack

- ASP.NET Core (.NET 9)
- Entity Framework Core
- SQLite
- Docker
- xUnit
- Swagger / OpenAPI
- MemoryCache

---

## 📌 API Endpoints

### 🔹 Create Short Link

**POST** `/api/links`

**Request:**

```json
{
  "originalUrl": "https://google.com"
}
```

**Response:**

```json
{
  "id": 1,
  "originalUrl": "https://google.com",
  "shortCode": "abc123",
  "shortUrl": "https://simplifylink-api.onrender.com/abc123"
}
```

---

### 🔹 Redirect

**GET** `/{shortCode}`

Example:

https://simplifylink-api.onrender.com/abc123

Redirects to the original URL and records click metadata.

---

### 🔹 Analytics

**GET** `/api/links/{id}/stats`

Returns:
- Click count
- Total click events
- Last 7-day breakdown

---

## 🗄 Database

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

## 🧪 Running Tests

```bash
dotnet test
```

Includes:
- URL validation tests
- ShortCode uniqueness tests
- Service logic tests

All tests currently passing.

---

## ▶️ Run Locally

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Open:

```
https://localhost:5292/swagger
```

---

## 🐳 Run via Docker

```bash
docker build -t simplifylink .
docker run -p 8080:8080 simplifylink
```

---

## 💡 Engineering Concepts Demonstrated

- RESTful API architecture
- Database normalization & indexing
- Async programming patterns
- Dependency injection
- Unit testing & separation of concerns
- Caching optimization
- Docker containerization
- Cloud deployment strategy
- Production-style backend engineering

---

## 👨‍💻 Author

Mohammed Akrama  
Bachelor of Information Technology (Class of 2026)  
Aspiring Software Development Engineer
