import React, { Component, useState, useEffect } from "react";
import { JokeResponse } from "../../types/autogen/selfcare-api-client";

export const JokePage = (props: any): JSX.Element | null => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchJokeData = async () => {
      const response = await fetch("https://localhost:7077/api/misc/joke");
      if (response.ok) {
        return await response.json();
      }
      return Promise.reject();
    };
    fetchJokeData()
      .then((resp) => {
        console.log(resp);
        setData(resp as JokeResponse);
        setLoading(false);
      })
      .catch((err) => console.log(err));
  }, []);

  return (
    <div>
      {!loading && <h1>{data?.joke}</h1>}
      {loading && <h1>I'm loading</h1>}
    </div>
  );
};

export default JokePage;
