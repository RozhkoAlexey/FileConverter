# FileConverter

- C#,
- ASP .NET Core8
- Entity Framework
- PostgreSQL
- React/Redux/JavaScript/TypeScript

Test task for converting HTML files to PDF.

The project consists of two applications:

1. Server part.
- The application will convert HTML files to PDF. It is possible to convert to other formats. Conversion is performed in different threads in parallel. There is a provision for starting conversion from non-converted files after restarting the application.

2. Client part.
- The application will send files to the server. Once submitted, requests are made to check the status. Application data is tied to the session. At the first launch, a Guid is created, which is saved in localStorage, it is the session key for accessing the converted files.
