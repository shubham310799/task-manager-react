import React from 'react'

export default function TaskCard(props) {
  const { id, name, description, status, dueDate } = props.task;
  console.log(id);
  var isDelayed = dueDate < new Date().toISOString().split('T')[0] && status !== "completed";
  return (
    <div className={`task-card ${isDelayed ? 'delayed-task' : ''}`}>
      <h3>Name: {name}</h3>
      <p>Description: {description}</p>
      <p>Status: {status}</p>
      <p>Due Date: {dueDate}</p>
    </div>
  )
}
