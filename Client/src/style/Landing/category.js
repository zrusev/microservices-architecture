import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
        '& > *': {
          margin: theme.spacing(1),
          width: theme.spacing(16),
          height: theme.spacing(16),
        },
    },
    title: {
        flexGrow: 1,
        color: 'textPrimary',
        margin: '0.5em',
        textAlign: 'center',
        fontSize: '1.2em',
        whiteSpace: 'break-spaces',
        [theme.breakpoints.up('sm')]: {
          display: 'block',
        },
    },
    icon: {
        display: 'block',
        fontSize: '4.5em',
        margin: 'auto',
    },
    item: {
      transition: 'all 0.2s linear',

      '&:hover': {
        textDecoration: 'none',
        borderRadius: '10px',
        boxShadow: '0 0 10px 10px rgb(0, 0, 0, 0.5)',
        marginTop: '0.1em',
        backgroundColor: 'lightgrey',
      },
    },
    paper: {
      minHeight: '9em',
    },
}));

export default useStyles;