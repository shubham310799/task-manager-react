import React, { useState } from 'react'
import { RiDeleteBinLine, RiEdit2Fill } from "react-icons/ri";
import TaskInput from './TaskInput';

export default function TaskCard(props) {

  const { id, name, description, status, dueDate } = props.task;
  const handleDeleteTask = props.handleDeleteTask;
  const updateTaskList = props.updateTaskList; 
  const [editTask, setEditTask] = useState(false);
  var isDelayed = dueDate < new Date().toISOString().split('T')[0] && status !== "completed";
  const [open, setOpen] = useState(false);
  return (
    <>
        <div className={`task-card ${isDelayed ? 'delayed-task' : ''}`}>
            <div className="task-card-header">
                <div className="task-card-header-name">
                    {name}
                </div>
                <div className="task-card-header-icons">
                    <RiEdit2Fill onClick={() => setOpen(true)}/>
                    <RiDeleteBinLine onClick={() => handleDeleteTask(id)}/>
                </div>                 
            </div>


           
            <div className="task-card-body">
                <p><span className="title">Description:</span> {description}</p>
                <p><span className="title">Status:</span> {status}</p>
                <p><span className="title">Due On:</span> {dueDate}</p>
            </div>
        </div>
        
        <TaskInput open={open} currTask={props.task} setHandleClose={setOpen} handleUpdate={updateTaskList} isEdit={true}></TaskInput>
    </>
  )
}
