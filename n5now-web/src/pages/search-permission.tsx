import { useEffect, useState } from 'react';
import { deletePermission, getAllPermissions } from '../services/permissionService';
import TextField from '@mui/material/TextField';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Alert, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControl, Grid, InputLabel, MenuItem, Select, Snackbar } from '@mui/material';
import { Link } from 'react-router-dom';
import { getAllPermissionTypes } from '../services/permissionTypeService';

function SearchPermission() {
    const [permissions, setPermissions] = useState<PermissionDto[]>([]);
    const [filteredPermissions, setFilteredPermissions] = useState<PermissionDto[]>([]);
    const [permissionTypes, setPermissionTypes] = useState<PermissionTypeDto[]>([]);
    const [selectedPermissionTypes, setSelectedPermissionTypes] = useState('none');
    const [selectedIdToDelete, setSelectedIdToDelete] = useState(0);
    const [searchTerm, setSearchTerm] = useState('');
    const [openSuccess, setOpenSuccess] = useState(false);
    const [openError, setOpenError] = useState(false);
    const [openDialog, setOpenDialog] = useState(false);


    const handleClose = () => {
        setOpenError(false);
        setOpenSuccess(false);
    };

    const handleCloseDialog = () => {
        setOpenDialog(false);
    };

    useEffect(() => {
        async function fetchPermissions() {
            try {
                const response = await getAllPermissions();
                setPermissions(response);
                setFilteredPermissions(response);
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
        fetchPermissions();
        fetchPermissionTypes();
    }, []);

    const handleSearchChange = (event: any) => {
        const searchTerm = event.target.value.toLowerCase();
        const filtered = permissions.filter((permission) => {
            const fullName = `${permission.employeeName} ${permission.employeeLastName}`.toLowerCase();
            return fullName.includes(searchTerm);
        });
        setFilteredPermissions(filtered);
        setSearchTerm(event.target.value);
    };
    const handlePermissionTypeChange = (event: any) => {
        const selectedPermissionTypesVaue = event.target.value;
        setSelectedPermissionTypes(selectedPermissionTypesVaue);
        if (selectedPermissionTypesVaue != "none") {
            const filtered = permissions.filter(x => x.permissionTypeId == parseInt(selectedPermissionTypesVaue))
            setFilteredPermissions(filtered);
        }
    };

    const handleDeletePermission = (id: number) => {
        setOpenDialog(true);
        setSelectedIdToDelete(id);
    }

    const handleConfirmDelete = async () => {
        try {
            const response = await deletePermission(selectedIdToDelete);
            console.log(response);
            if (response) {
                setOpenDialog(false);
                setOpenSuccess(true);
                const response = await getAllPermissions();
                setPermissions(response);
                setFilteredPermissions(response);

            } else {
                setOpenDialog(false);
                setOpenError(true);
            }
        }
        catch (error) {
            console.error(error);
            setOpenDialog(false);
            setOpenError(true);
        }
    }


    return (
        <div>
            <h2>Permissions</h2>
            <Grid container spacing={2} alignItems="center">
                <Grid item xs={4}>
                    <TextField
                        label="Search"
                        variant="standard"
                        value={searchTerm}
                        onChange={handleSearchChange}
                        className="search-text-field"
                        InputProps={{
                            endAdornment: (
                                <span className="material-symbols-outlined">search</span>
                            ),
                        }}
                    />
                </Grid>
                <Grid item xs={4} style={{ textAlign: 'center' }}>
                    <FormControl sx={{ m: 1, minWidth: 200 }}>
                        <InputLabel id="demo-simple-select-label">Permission Type</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={selectedPermissionTypes}
                            label="Permission Type"
                            onChange={handlePermissionTypeChange}
                        >
                            <MenuItem value="none">
                                None
                            </MenuItem>
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
                <Grid item xs={4} style={{ textAlign: 'right' }}>
                    <Link to="/permission/create/0" className="link-button">
                        <Button variant="contained">
                            <span className="material-symbols-outlined">add</span>
                            Add Permission
                        </Button>
                    </Link>
                </Grid>
            </Grid>

            <TableContainer component={Paper} className='table'>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Last Name</TableCell>
                            <TableCell>Permission Type</TableCell>
                            <TableCell>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {filteredPermissions.map((permission: PermissionDto) => (
                            <TableRow key={permission.id}>
                                <TableCell>{permission.id}</TableCell>
                                <TableCell>{permission.employeeName}</TableCell>
                                <TableCell>{permission.employeeLastName}</TableCell>
                                <TableCell>{permission.permissionTypeDescription}</TableCell>
                                <TableCell>
                                    <Link to={`/permission/edit/${permission.id}`}>
                                        <span className="material-symbols-outlined icon-edit">edit</span>
                                    </Link>
                                    <span className="material-symbols-outlined icon-delete" onClick={() => handleDeletePermission(permission.id!)}>delete</span>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Snackbar open={openSuccess} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity="success" sx={{ width: '100%' }}>
                    Deleted Permission!
                </Alert>
            </Snackbar>
            <Snackbar open={openError} autoHideDuration={6000} onClose={handleClose}>
                <Alert onClose={handleClose} severity="error" sx={{ width: '100%' }}>
                    Error when deleting permission!
                </Alert>
            </Snackbar>
            <Dialog
                open={openDialog}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {"Delete Permission?"}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        Are you sure you want to remove the permission?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleConfirmDelete}>
                        <span className="material-symbols-outlined">
                            check
                        </span>Confirm</Button>
                    <Button onClick={handleCloseDialog}>
                        <span className="material-symbols-outlined">
                            cancel
                        </span>
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        </div >
    )
}

export default SearchPermission
