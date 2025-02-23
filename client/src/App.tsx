import { List, ListItem, ListItemText, Typography } from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";

//defines a functional component named App
function App() {
  //creates a state variable activities and a function setActivities to update it
  //--The initial value of activities is an empty array []
  const [activities, setActivities] = useState<Activity[]>([]);

  //useEffect is a side effect hook that runs after the component renders. 
  // --Here it is used to fetch data from an API
  //the first .then statement converts the results of the fetch to JSON
  //the second .then statement passes this JSON to setActivities which updates the activities state
  useEffect(() => {
    axios.get<Activity[]>('https://localhost:5001/api/activities')
    .then(response => setActivities(response.data))
  },[])

  //the empty dependency array [] tells React to run this effect only once

  return (
    <>
      <Typography variant='h3'>Reactivities</Typography>
      <List>
          {activities.map((activity) => (
            <ListItem key={activity.id}>
              <ListItemText>{activity.title}</ListItemText>
            </ListItem>
          ))}
      </List>
    </>
  )
}

export default App
