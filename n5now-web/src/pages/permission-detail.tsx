import { useEffect, useState } from 'react';
import { createPermission, getPermissionById, updatePermission } from '../services/permissionService';
import { Button, TextField, Grid, InputLabel, FormControl, Select, MenuItem, Snackbar, Alert } from '@mui/material';
import { useParams } from 'react-router-dom';
import { getAllPermissionTypes } from '../services/permissionTypeService';
import { useNavigate } from 'react-router-dom';

export default function PermissionDetail() {
    const { action, id } = useParams();
    const [employeeName, setEmployeeName] = useState('');
    const [employeeLastName, setEmployeeLastName] = useState('');
    const [permissionTypeId, setPermissionTypeId] = useState('');
    const [permissionTypes, setPermissionTypes] = useState<PermissionTypeDto[]>([]);
    const [openSuccess, setOpenSuccess] = useState(false);
    const [openError, setOpenError] = useState(false);

    const navigate = useNavigate();

    const handleClose = () => {
        setOpenError(false);
        setOpenSuccess(false);
    };


    useEffect(() => {
        async function fetchPermission() {
            try {
                if (action === "edit" && id != "0") {
                    const response = await getPermissionById(parseInt(id!));
                    setEmployeeName(response.employeeName);
                    setEmployeeLastName(response.employeeLastName);
                    setPermissionTypeId(response.permissionTypeId.toString());
                }
            } catch (error) {
                console.error(error);
            }
        }

        async function fetchPermissionTypes() {
            try {
                const response = await getAllPermissionTypes();
                setPermissionTypes(response);
            } catch (error) {
                console.error(error);
            }
        }
        fetchPermission();
        fetchPermissionTypes();
    }, []);

    const handleEmployeeNameChange = (event: any) => {
        setEmployeeName(event.target.value);
    };

    const handleEmployeeLastNameChange = (event: any) => {
        setEmployeeLastName(event.target.value);
    };

    const handleSubmit = async (event: any) => {
        event.preventDefault();

        try {
            const permissionData: PermissionDto = {
                employeeName,
                employeeLastName,
                permissionTypeId: parseInt(permissionTypeId, 10), // Convert to integer
            };

            if (action === "create") {
                const response = await createPermission(permissionData);
                console.log(response);
            }
            else {
                permissionData.id = parseInt(id!);
                const response = await updatePermission(permissionData);
                console.log(response);
            }

            setEmployeeName('');
            setEmployeeLastName('');
            setPermissionTypeId('');
        } catch (error) {
            console.error(error);
            setOpenError(true);
        } finally {
            navigate('/');
        }
    };

    const handlePermissionTypeChange = (event: any) => {
        const selectedPermissionTypesVaue = event.target.value;
        if (selectedPermissionTypesVaue != "none") {
            setPermissionTypeId(selectedPermissionTypesVaue);
        }
    };

    return (
        <div>
            <h2> {action === "create" ? "Create Permission" : "Edit Permission"}</h2>
            <form onSubmit={handleSubmit}>
                <Grid container spacing={2}>
                    <Grid item xs={10}>
                        <TextField
                            label="Employee Name"
                            variant="outlined"
                            fullWidth
                            required
                            value={employeeName}
                            onChange={handleEmployeeNameChange}
                        />
                    </Grid>
                    <Grid item xs={10}>
                        <TextField
                            label="Employee Last Name"
                            variant="outlined"
                            fullWidth
                            required
                            value={employeeLastName}
                            onChange={handleEmployeeLastNameChange}
                        />
                    </Grid>
                    <Grid item xs={10}>
                        <FormControl sx={{ marginBottom: 1, minWidth: 200 }}>
                            <InputLabel id="demo-simple-select-label">Permission Type</InputLabel>
                            <Select
                                labelId="demo-simple-select-label"
                                id="demo-simple-select"
                                value={permissionTypeId}
                                label="Permission Type"
                                onChange={handlePermissionTypeChange}
                            >
                                {permissionTypes.map((permissionType: PermissionTypeDto) => (
                                    <MenuItem
                                        key={permissionType.id}
                                        value={permissionType.id}
                                    >
                                        {permissionType.description}
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                </Grid>
                <Button
                    type="submit"
                    variant="contained"
                    color="success"
                >
                    Register
                </Button>
            </form>
            <Snackbar open={openSuccess} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity="success" sx={{ width: '100%' }}>
                    Saved Permission!
                </Alert>
            </Snackbar>
            <Snackbar open={openError} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity="error" sx={{ width: '100%' }}>
                    Error when saving permission!
                </Alert>
            </Snackbar>
        </div>
    );
}
