# 🌍 Ametia API - Empowering Tourists in Egypt

Ametia is a powerful RESTful API built with ASP.NET Core designed to support a Flutter-based tourism mobile application. It provides essential data and services to enhance the travel experience for tourists visiting Egypt.

---

## 🚀 Features

- 🏨 Hotel Listings  
- 🏛️ Historical & Tourist Attractions  
- 👨‍👩‍👧‍👦 Family-Friendly Destinations  
- 🎉 Entertainment Venues  
- 💰 Banking Services  
- 🏢 Embassy Information  
- 🍽️ Restaurants  
- 🚌 Transport Providers  
- 🌆 Cities  
- 👥 User Authentication System

---

## ⚙️ System Requirements

- [.NET 6 / 7+](https://dotnet.microsoft.com/)
- SQL Server
- Entity Framework Core
- Docker / Windows / Linux (optional)

---

## 📥 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/Ametia-API.git
cd Ametia-API
```

### 2. Database Migration

```bash
dotnet ef database update
```

### 3. Run the Server

```bash
dotnet run
```

---

## 📑 API Documentation

Once the server is running, visit:

```
http://localhost:{5001}/swagger
```

Use Swagger UI to test endpoints interactively.

---

## 🛠️ Tech Stack

- **C# & ASP.NET Core** – RESTful API
- **SQL Server** – Database
- **Entity Framework Core** – ORM
- **JWT / Session-Based Auth** – Authentication
- **Swagger UI** – API Documentation

---

## 🔐 Auth Endpoints

| Method | Endpoint              | Description                 |
|--------|-----------------------|-----------------------------|
| POST   | `/api/User/Register`  | Register new user           |
| GET    | `/api/User/Login`     | Login existing user         |
| GET    | `/api/User/Forgot Password` | Forgot password         |
| GET    | `/api/User/Load User` | Get current user data       |
| POST   | `/api/User/Update`    | Update user details         |
| GET    | `/api/User/Get All User` | Get all users (admin)   |
| DELETE | `/api/User/Delete User?id={id}` | Delete a user     |

---

## 🌐 Main Endpoints Overview

| Resource             | Endpoint Prefix        | Description                         |
|----------------------|------------------------|-------------------------------------|
| Banks                | `/api/Bank`            | Create, update, delete, get banks   |
| Cities               | `/api/City`            | Manage city data                    |
| Hotels               | `/api/Hotels`          | Manage hotel data                   |
| Embassies            | `/api/Embassies`       | Manage embassy information          |
| Restaurants          | `/api/Resturant`       | Manage restaurants                  |
| Entertainment Places | `/api/EntertainmentPlace` | Manage entertainment venues   |
| Tourist Places       | `/api/TourismPlace`    | Manage tourist attractions          |
| Transport Providers  | `/api/Services`        | Manage transport services           |
| Destination Filter   | `/api/Distnation`      | Top-rated filtering by city         |
| Types of Places      | `/api/TypePlaces`      | Manage types of tourist places      |

---

## 🎥 Project Walkthrough Video

You can watch the full walkthrough here:  
👉 [Watch the project explanation video](https://github.com/ahmed-mahmoud-090/GraduationProject/blob/main/Screen%20Recording%20(7-1-2025%201-12-26%20AM).gif)

---

## 🤝 Contributing

1. Fork the repo
2. Create a new branch (`git checkout -b feature-name`)
3. Commit your changes (`git commit -m "Add feature"`)
4. Push to the branch (`git push origin feature-name`)
5. Open a Pull Request

---

## 📧 Contact

If you have questions or feedback, feel free to open an issue or contact the maintainer.

---

> Made with 💙 by the Ametia Dev Team
1-Ahmed Mahmoud Selim
2-Andrew Magdy Fayez 
3-Ahmed Mohamed Mohamed
4-Beshoy Boktor 
6-Kerolos Nashat 
