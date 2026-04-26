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
	const [open, setOpen] = useState(false);
	const [tabValue, setTabValue] = React.useState("all");

  const tasks = taskList.filter((task) => {
    switch (tabValue) {
			case "pending":
			case "in-progress":
			case "completed":
				return task.status === tabValue;
				break;
			case "delayed":
				return task.dueDate < new Date().toISOString().split("T")[0] && task.status !== "completed";
				break;
			default:
				return true;
		}
  });


	const handleOpen = () => setOpen(true);
	const handleClose = () => setOpen(false);
	const handleSubmit = () => {
		// handle form data here
		setOpen(false);
	};

	const HandleUpdateTaskList = (data) => {
		var updated = [];
		if (taskList.find((task) => task.id === data.id)) {
			updated = taskList.map((task) => (task.id === data.id ? data : task));
		} else {
			updated = [...taskList, data];
		}
		setTaskList(updated);
	};

	const handleDeleteTask = (id) => {
		var updated = taskList.filter((task) => task.id !== id);
		setTaskList(updated);
    handleTabChange(null, tabValue);
	};

	const handleTabChange = (event, newValue) => {
		setTabValue(newValue);
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
							onChange={handleTabChange}
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
				isEdit={false}
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
