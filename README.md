# User Management App (WPF + PostgreSQL + EF Core)

This application is a simple user management system built with **WPF**, **Entity Framework Core**, and **PostgreSQL**.

---

## 🐘 PostgreSQL Setup

### 1. Open your PostgreSQL terminal (psql), and run the following:

```sql
-- Create the database
CREATE DATABASE usermanagement;

-- Connect to the database
\c usermanagement;

-- Create the users table (no quotes, lowercase)
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL
);

-- Insert test data (optional)
INSERT INTO users (username, password) VALUES ('testuser', 'password123');
