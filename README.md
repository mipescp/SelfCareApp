# SelfCareApp
 SelfCareApp

Goal for this application is to provide a simple SelfCare application protected with a simple Login/SignUp form 

- Backend: .NET 7
- Frontend: React Typescript

Entry point for application is located in SelfCare.Host. When you run the application it will open a webpage with a React frontend.

React application is located in SelfCare.Host/ClientApp.

Steps to run the application
1. Get into SelfCare.Host/ClientApp and run "npm install" at the root.
2. Go back to SelfCare.Host and open SelfCare.Host.sln using your desired editor (recommend using Visual Studio)
3. Start the application, choosing SelfCare.Host as your starting project.
4. Profit?


Features:
- Connection with MongoDb to user management
- MediatR design pattern on Backend
- Easy E2E integration between Backend and Frontend
- Usage of Automapper
- Usage of NSwag extention for auto client generation (to be consumed by frontend with command "npm run nswag:local")
- 2 "Modules", one that displays a random joke and another 
- Protected routes on the react application
- JWT Bearer authentication integration between backend and frontend
- Protected routes on backend. [Authorized] endpoints.
- Add CORS Policy allowing only specific hosts
- Swagger UI available at https://localhost:7077/swagger

Future Features (TBD):
- Improve UX/UI (it's really uglish right now)
- Add preferences page to choose which type of jokes to show
- Update profile page
- More widgets (modules)
