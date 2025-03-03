import { Box, Container, CssBaseline} from "@mui/material";
import NavBar from "./NavBar";
import { Outlet } from "react-router";

//defines a functional component named App
function App() {


  

  //the empty dependency array [] tells React to run this effect only once

  return (
    <Box sx={{bgcolor: '#eeeeee', minHeight: '100vh'}}>
      <CssBaseline />
      <NavBar />
      <Container maxWidth='xl' sx={{mt: 3}}>
        <Outlet />
      </Container>
    </Box>
  )
}

export default App
