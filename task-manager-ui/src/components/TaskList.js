import React, { useState } from 'react'
import TaskCard from './TaskCard'
import TaskInput from './TaskInput'
import { IoAddCircleOutline } from "react-icons/io5";

export default function TaskList() {
    const taskList = [
        {
            id: 1,
            name: "my task1",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        },
        {
            id:2,
            name: "my task2",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        },
        {
            id:3,
            name: "my task3",
            description: "this is my task",
            status: "pending",
            dueDate: "2024-06-30"
        }
    ];
    const [open, setOpen] = useState(false);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleSubmit = () => {
        // handle form data here
        setOpen(false);
    };
    const [tasks, setTasks] = useState(taskList);

    const HandleAddTask = (data) =>{
        setTasks([...tasks, data])
    }

    var max=tasks.length>0 ? tasks.reduce((prev, curr)=>{
            return curr.id>prev.id? curr:prev;
        }) : { id: 0 };

        console.log("max=",max.id);

  return (
    <>
        <TaskInput open={open} id={max.id+1} setHandleClose={setOpen}  handleAdd={HandleAddTask}></TaskInput>
        <div className="task-list">  
            {
                tasks.length>0 && tasks.map(task =>{
                    return <TaskCard task={task} key={task.id}></TaskCard>
                })
            }
        <div>
      {/* Button */}
      <div className="task-list-add-task-icon">
            <IoAddCircleOutline onClick={handleOpen} />
      </div>
        
        </div>
        </div>
    </>
    
    
  )
}
