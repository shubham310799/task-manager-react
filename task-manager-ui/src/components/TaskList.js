import React, { useState, useEffect } from "react";
import TaskCard from "./TaskCard";
import TaskInput from "./TaskInput";
import { IoAddCircleOutline } from "react-icons/io5";
import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import TabContext from "@mui/lab/TabContext";
import TabList from "@mui/lab/TabList";
import TabPanel from "@mui/lab/TabPanel";
import { GridLoader } from "react-spinners";

export default function TaskList() {
	const [taskList, setTaskList] = useState([]);
	const [open, setOpen] = useState(false);
	const [tabValue, setTabValue] = React.useState("all");
	const [loading, setLoading] = useState(true);
	const [editId, setEditId] = useState(null);
	const [isEdit, setIsEdit] = useState(false);
	const override = {
		display: "block",
		margin: "0 auto",
		borderColor: "#0328fa",
	};
	const tasks = taskList.filter((task) => {
		switch (tabValue) {
			case "pending":
			case "in-progress":
			case "completed":
				return task.status === tabValue;
				break;
			case "delayed":
				return (
					task.dueDate < new Date().toISOString().split("T")[0] &&
					task.status !== "completed"
				);
				break;
			default:
				return true;
		}
	});

	useEffect(() => {
		fetch("https://localhost:7131/api/task/all")
			.then((response) => response.json())
			.then((data) => {
				setTaskList(data);
				setLoading(false);
			})
			.catch((error) => {
				console.error("Error fetching tasks:", error);
				setLoading(false);
			});
	}, []);

	const handleOpen = () => setOpen(true);
	const handleClose = () => setOpen(false);
	const handleSubmit = () => {
		// handle form data here
		setOpen(false);
	};

	const HandleUpdateTaskList = (data) => {
		var updated = taskList;
		if (taskList.find((task) => task.id === data.id)) {
      setIsEdit(true);
      setEditId(data.id);
			setTaskList(taskList.map((task) => (task.id === data.id ? data : task)));
		} else {
      setIsEdit(true);
			fetch("https://localhost:7131/api/task/add", {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify(data),
			})
				.then((response) => response.json())
				.then((data) => setTaskList(data))
				.catch((error) => console.error("Error:", error));
		}
    setIsEdit(false);
    setEditId(null);
	};

	const handleDeleteTask = (id) => {
    setIsEdit(true);
    setEditId(id);
		fetch(`https://localhost:7131/api/task/${id}`, {
			method: "DELETE",
		})
			.then((response) => response.json())
			.then((data) => setTaskList(data))
			.catch((error) => console.error("Error:", error));

    setIsEdit(false);
    setEditId(null);
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

	return !loading ? (
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
						return !(isEdit && editId == task.id) ? (
							<TaskCard
								task={task}
								key={task.id}
								handleDeleteTask={handleDeleteTask}
								updateTaskList={HandleUpdateTaskList}
								isEdit={false}
							></TaskCard>
						) : (
							<GridLoader
								color="#0328fa"
								loading={loading}
								cssOverride={override}
								size={20}
								aria-label="Loading Spinner"
								data-testid="loader"
							/>
						);
					})}
				<div>
					<div className="task-list-add-task-icon">
						<IoAddCircleOutline onClick={handleOpen} />
					</div>
				</div>
			</div>
		</>
	) : (
		<GridLoader
			color="#0328fa"
			loading={loading}
			cssOverride={override}
			size={20}
			aria-label="Loading Spinner"
			data-testid="loader"
		/>
	);
}
