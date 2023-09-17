import './App.css'
import { Container } from '@mui/material'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import SearchPermission from './pages/search-permission.tsx';
import PermissionDetail from './pages/permission-detail.tsx';

function App() {

  return (
    <Container>
      <Router>
        <Routes>
          <Route path="/" element={<SearchPermission />} />
          <Route path="/permission/:action/:id" element={<PermissionDetail />} />
        </Routes>
      </Router>
    </Container>
  )
}

export default App
