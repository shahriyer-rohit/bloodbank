# 🩸 BloodBankDB

A **Blood Bank Management System** built with **ASP.NET Core MVC, Entity Framework Core, and SQL Server**. The application provides an easy-to-use platform for managing blood donors and donation records.

## ✨ Features

* 👤 Donor management
* ➕ Add new donors
* ✏️ Edit donor information
* 🔍 View donor details
* 🗑️ Delete donor records
* 🩸 Donation management
* 📅 Track donation dates
* 💉 Record donated blood volume
* 🔎 Filter donors by blood group
* 📊 View donor donation counts
* 📈 Calculate total blood volume
* 🗓️ Sort donors by last donation date
* 📱 Responsive Bootstrap interface
* ✅ Form validation

## 🛠️ Tech Stack

* **ASP.NET Core MVC**
* **C#**
* **Entity Framework Core**
* **SQL Server**
* **Razor Views**
* **Bootstrap**
* **LINQ**

## 🚀 Getting Started

### Prerequisites

Make sure you have installed:

* .NET SDK
* SQL Server
* Visual Studio or VS Code
* Entity Framework Core Tools

### 1. Clone the Repository

```bash
git clone <repository-url>
cd BloodBankDB
```

### 2. Configure Database

Update the connection string in:

```text
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BloodBankDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

Replace `YOUR_SERVER` with your SQL Server instance.

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Update Database

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

Or open the project in Visual Studio and press **F5**.

## 🎥 Demo

Add your project demo video here:

```markdown
[![BloodBankDB Demo](https://img.youtube.com/vi/YOUR_VIDEO_ID/0.jpg)](https://www.youtube.com/watch?v=YOUR_VIDEO_ID)
```

## 📸 Screenshots

Add screenshots of the main application screens here.

## 📄 License

This project is developed for educational and portfolio purposes.
