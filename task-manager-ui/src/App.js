import React from 'react';
import TaskList from './components/TaskList';
import LoginPage from './components/LoginPage';
import './App.css';
import { Route, Routes } from 'react-router-dom';
export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/task-list" element={<TaskList />} />
    </Routes>

  );
}
