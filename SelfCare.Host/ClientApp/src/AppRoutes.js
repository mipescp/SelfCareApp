import JokePage from "./components/JokePage.tsx";
import Home from "./components/Home";
import Login from "./components/Login.tsx";
import Registration from "./components/Registration.tsx";

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/registration",
    element: <Registration />,
  },
  {
    path: "/joke",
    element: <JokePage />,
  },
];

export default AppRoutes;
