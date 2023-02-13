import React, { Component, useState, useEffect } from "react";
import { JokeResponse } from "../../types/autogen/selfcare-api-client";
import JokeBanner from "./JokeBanner.tsx";
import { useAuthHeader } from "react-auth-kit";
import { Paper, Typography } from "@mui/material";

export const Home = (props: any): JSX.Element | null => {
  const authHeader = useAuthHeader();

  const [userDetail, setUserDetail] = useState(null);

  useEffect(() => {
    const fetchJokeData = async () => {
      const response = await fetch("https://localhost:7077/api/user/details", {
        headers: {
          Authorization: authHeader(),
        },
      });
      if (response.ok) {
        return await response.json();
      }
      return Promise.reject();
    };
    fetchJokeData()
      .then((resp) => {
        setUserDetail(resp);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div>
      <JokeBanner />
      <Paper elevation={10} sx={{ mt: "20px" }}>
        <Typography variant="h3">
          Welcome back {userDetail?.displayName}
        </Typography>
      </Paper>
    </div>
  );
};

export default Home;
