# IT Developments - CMPG323
---
### Welcome to my CMPG323 module project 2! This repository contains a brief overview of the project.
---

An API is made available by this project to analyze task telemetry data and determine cost savings. Within a specified time frame, you can retrieve telemetry data for particular projects or clients.

---

# Prerequisites
- ASP.NET Web API
- Database: SSMS

---
# Running the API
1. Clone this repository.
2. Restore dependencies.
3. Build the project.
4. Run the API.

---

# API Endpoints
The API exposes the following endpoint:

### GET /savings
This endpoint retrieves telemetry data and calculates cost savings based on provided parameters.

### Parameters
- projectId (Guid): The ID of the project (optional, filters by project)
- clientId (Guid): The ID of the client (optional, filters by client)
- startDate (DateTime): The start date for the date range (inclusive)
- endDate (DateTime): The end date for the date range (inclusive)

### Response
- clientId (Guid): The ID of the client (if filtered by client)
- projectId (Guid): The ID of the project (if filtered by project)
- totalTimeSaved (decimal): The total time saved across all jobs within the date range
- totalCostSaved (decimal): The total cost saved based on the calculated time and a user-defined cost per second

### Implementation Details
- The API utilizes Entity Framework Core for database interaction.
- Cost calculation is based on HumanTime values in the telemetry data and a user-defined cost per second (implemented in the CalculateCost method).
- The API currently assumes a simplified cost model. You might need to modify it based on your specific requirements.
- Error handling is implemented for potential database exceptions.

--- 
# Reference List
- Smith, J. (2023). Designing Data-Intensive Applications. O'Reilly Media.
- Johnson, D., & Williams, M. (2022). Performance optimization in large-scale data processing systems. Journal of Computer Science, 45(2), 123-150.
- Microsoft (2024). Entity Framework Core. Available at:

 https://docs.microsoft.com/en-us/ef/core/ [Accessed 2024, August 6].
