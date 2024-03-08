# Design Document

## 1. Client-Server Communication:

### Considered Options:
- WebSockets
- REST API (HTTP Polling)
- SSE

### Chosen Approach: REST API (HTTP Polling)

### Rationale:
The primary requirement involves updating the chart every minute, placing more emphasis on client-side behavior. While WebSockets excel in real-time communication, REST APIs offer several advantages for this specific scenario:

- **Scalability:** REST APIs are well-suited for handling numerous client connections due to their stateless nature.
- **Maturity and Simplicity:** REST APIs are widely adopted and understood, simplifying development and integration.
- **Alignment with Requirement:** REST APIs effectively fulfill the need for periodic data updates without the complexity of real-time push mechanisms from the server.
  WebSockets are recommended only when there is a real need for bi-drectional communication. SSE might not fulfill the requirement as the client should be able to a request every time user uploads a file.

## 2. Data Storage:

### Considered Options:
- Database (SQL/NoSQL)
- In-Memory Storage

### Chosen Approach: In-Memory Storage

### Rationale:
The application only requires the processed data from the most recently uploaded file for randomization and client-side updates. In-memory storage offers several benefits in this context:

- **Simplicity & Performance:** In-memory storage eliminates the overhead of setting up and managing a database, streamlining development. In-memory access is significantly faster than database interactions for the requirement.
- **Alignment with Requirement:** For this specific use case, where data persistence is not a critical requirement, in-memory storage provides a sufficient and efficient solution.

## 3. API Design:

### Considered Options:
- Synchronous vs. Asynchronous for the API

### Chosen Approach: Synchronous for HttpGet updated-data API and Async for parse-file API.

### Rationale:
The 'updated-data' API retrieves processed data and does not involve any external I/O operations. A synchronous approach offers advantages like Simplicity and Clarity. 
The 'parse-file' API involves async operation like file reading, due to which keeping it Asynchronous makes more sense.

## 4. UI Framework and Library:

### Chosen Framework: Angular

### Rationale:
Angular offers several advantages as a UI framework like Component-Based Architecture, easy implementation, Rich Ecosystem and its Data Binding Feature (Simplifies data flow management between the view and model).

### Considered UI Libraries: 
- PrimeNG
- Angular Material

### Chosen UI Library: PrimeNG

### Rationale:
PrimeNG complements Angular with several benefits:

- **Large Component Set:** Provides an extensive range of pre-built UI elements.
- **Lightweight and Modular:** Uses a modular design for selective component inclusion, keeping the application footprint small.
- **Active Community:** Supported by a large and active developer community.
