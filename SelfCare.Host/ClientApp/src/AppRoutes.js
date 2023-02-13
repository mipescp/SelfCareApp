import Home from "./components/Home.tsx";
import Login from "./components/Login.tsx";
import Logout from "./components/Logout.tsx";
import Registration from "./components/Registration.tsx";
import { RequireAuth } from "react-auth-kit";

const AppRoutes = [
  {
    index: true,
    element: (
      <RequireAuth loginPath="/login">
        <Home />
      </RequireAuth>
    ),
  },
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/logout",
    element: (
      <RequireAuth loginPath="/login">
        <Logout />{" "}
      </RequireAuth>
    ),
  },
  {
    path: "/registration",
    element: <Registration />,
  },
];

export default AppRoutes;
