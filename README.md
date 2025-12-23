# Centralized Production Monitoring System

A high-performance Industrial IoT (IIoT) monitoring dashboard designed to capture, log, and visualize machine statuses and error profiles from **Mitsubishi FX5U PLCs**. This project was developed as part of an industrial digitalization initiative during an internship at **PT Sharp Electronics Indonesia**.

## 📌 Project Overview
This system serves as a real-time bridge between the shop floor (PLC) and the engineering level (Web Dashboard). It automates the data acquisition process, transforming raw PLC register data into actionable insights stored in a centralized MySQL database.

**Key Objective:** To provide comprehensive monitoring for **all machines in the production line**



## 🚀 Key Features
* **Direct PLC Integration:** High-speed data polling using the **MC Protocol (SLMP)** for low-latency communication.
* **Automated Event Logging:** Automatically captures error codes, descriptions, and timestamps from PLC D-Registers.
* **Interactive Web Dashboard:** Modern UI built with **Blazor Server**, featuring a responsive and collapsible sidebar for optimized viewing.
* **Condition Monitoring:** Instant visual feedback (Digital Andon) for "Normal" and "Trouble" machine states.
* **Centralized Data:** Eliminates manual paperwork by logging all production anomalies into a structured SQL database.

## 🛠️ Tech Stack
| Component | Technology |
| :--- | :--- |
| **Industrial Hardware** | Mitsubishi FX5U PLC |
| **Connectivity** | **MC Protocol (SLMP)** |
| **Middleware / ETL** | Node-RED & `node-red-contrib-mcprotocol` |
| **Database** | MySQL (XAMPP / DBngin) |
| **Web Framework** | .NET 8 (Blazor Server) |
| **UI/UX** | CSS3, Bootstrap Icons |

## 📐 System Architecture
1.  **Shop Floor (PLC):** The Mitsubishi FX5U PLC monitors machine sensors and manages error flags in specific registers (**D200 - D399**) from another PLCs.
2.  **Acquisition Layer (Node-RED):** Node-RED polls the PLC directly via Ethernet using the `node-red-contrib-mcprotocol` library.
3.  **Processing:** Node-RED maps register values to human-readable error names, filters redundant data, and executes SQL commands.
4.  **Persistence Layer (SQL):** A MySQL database stores every event with a `timestamp`, `nama_mesin_proses`, and `data_timer`.
5.  **Presentation Layer (Web):** The Blazor dashboard fetches data via ADO.NET/Dapper to provide a real-time monitoring interface for Production Engineers.



## 💻 Setup & Installation

### 1. Database Configuration
Run the following SQL script to create the necessary table:
```sql
CREATE TABLE centralized_logs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nama_mesin_proses VARCHAR(255),
    data_timer INT,
    waktu DATETIME
);

```

### 2. Web Application

Update the connection string in appsettings.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=centralized_logs;User=root;Password=;"
}
```
### 3. Run the Application
```
dotnet watch run
```
### Node-RED Flow

Install `node-red-contrib-mcprotocol`.

Configure the MC Protocol node with your PLC's IP Address and Port (Default: 5010/5012).

Deploy the flow to start logging data.
