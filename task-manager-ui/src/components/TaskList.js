import React, { useState } from "react";
import TaskCard from "./TaskCard";
import TaskInput from "./TaskInput";
import { IoAddCircleOutline } from "react-icons/io5";
import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";

export default function TaskList() {
  const [taskList, setTaskList] = useState([
    {
      id: 1,
      name: "my task1",
      description: "this is my task",
      status: "pending",
      dueDate: "2024-06-30",
    },
    {
      id: 2,
      name: "my task2",
      description: "this is my task",
      status: "pending",
      dueDate: "2024-06-30",
    },
    {
      id: 3,
      name: "my task3",
      description: "this is my task",
      status: "pending",
      dueDate: "2024-06-30",
    },
  ]);
  const [tasks, setTasks] = useState(taskList);
  const [open, setOpen] = useState(false);

  const [tabValue, setTabValue] = React.useState("all");

  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const handleSubmit = () => {
    // handle form data here
    setOpen(false);
  };

  const HandleUpdateTaskList = (data) => {
    var updated = [];
    if (tasks.find((task) => task.id === data.id)) {
      updated = taskList.map((task) => (task.id === data.id ? data : task));
    } else {
      updated = [...taskList, data];
    }
    setTaskList(updated);
    setTasks(updated);
  };

  const handleDeleteTask = (id) => {
    var updated = taskList.filter((task) => task.id !== id);
    setTaskList(updated);
    setTasks(updated);
  };

  const handeTabChange = (event, newValue) => {
    setTabValue(newValue);
    console.log(taskList);
    switch (newValue) {
      case "pending":
      case "in-progress":
      case "completed":
        setTasks(taskList.filter((task) => task.status === newValue));
        break;
      default:
        setTasks(taskList);
    }
  };

  var max =
    tasks.length > 0
      ? tasks.reduce((prev, curr) => {
          return curr.id > prev.id ? curr : prev;
        })
      : { id: 0 };

  const newTask = {
    id: max.id + 1,
    name: "",
    description: "",
    dueDate: "",
    status: "pending",
  };

  return (
    <>
      <Box sx={{ width: "100%", typography: "body1" }}>
        <TabContext value={tabValue}>
          <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
            <TabList
              onChange={handeTabChange}
              aria-label="lab API tabs example"
            >
              <Tab label="All" value="all" />
              <Tab label="Pending" value="pending" />
              <Tab label="In Progress" value="in-progress" />
              <Tab label="Completed" value="completed" />
            </TabList>
          </Box>
        </TabContext>
      </Box>
      <TaskInput
        open={open}
        currTask={newTask}
        setHandleClose={setOpen}
        handleUpdate={HandleUpdateTaskList}
      ></TaskInput>
      <div className="task-list">
        {tasks.length > 0 &&
          tasks.map((task) => {
            return (
              <TaskCard
                task={task}
                key={task.id}
                handleDeleteTask={handleDeleteTask}
                updateTaskList={HandleUpdateTaskList}
                isEdit={false}
              ></TaskCard>
            );
          })}
        <div>
          <div className="task-list-add-task-icon">
            <IoAddCircleOutline onClick={handleOpen} />
          </div>
        </div>
      </div>
    </>
  );
}
