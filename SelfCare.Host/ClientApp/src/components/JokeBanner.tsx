import { Watch } from "@mui/icons-material";
import { Paper, Skeleton, Typography } from "@mui/material";
import { maxWidth } from "@mui/system";
import React, { Component, useState, useEffect } from "react";
import { useAuthHeader } from "react-auth-kit";
import { JokeResponse } from "../../types/autogen/selfcare-api-client";

export const JokeBanner = (props: any): JSX.Element | null => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  const authHeader = useAuthHeader();

  useEffect(() => {
    const fetchJokeData = async () => {
      const response = await fetch("https://localhost:7077/api/misc/joke", {
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
        setData(resp as JokeResponse);
        //Just to be able to see loading state
        setTimeout(() => setLoading(false), 1500);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div>
      {!loading && (
        <Paper elevation={5}>
          <Typography variant="h5">Pun: {data?.joke}</Typography>
        </Paper>
      )}
      {loading && <Skeleton variant="rectangular" width={"90%"} height={70} />}
    </div>
  );
};

export default JokeBanner;
