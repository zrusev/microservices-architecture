import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    icon: {
      marginRight: theme.spacing(2),
    },
    heroContent: {
      backgroundColor: theme.palette.background.paper,
      backgroundImage: 'linear-gradient(to top, darkgrey, white)',
      padding: theme.spacing(8, 0, 6),
    },
    heroContentBottom: {
      backgroundColor: theme.palette.background.paper,
      backgroundImage: 'linear-gradient(to bottom, darkgrey, white)',
      padding: theme.spacing(8, 0, 6),
    },
    darkContent: {
      backgroundColor: theme.palette.background.paper,
      backgroundImage: 'none',
      padding: theme.spacing(8, 0, 6),
    },
    heroButtons: {
      marginTop: theme.spacing(4),
    },
    cardGrid: {
      paddingTop: theme.spacing(8),
      paddingBottom: theme.spacing(8),
    },
    card: {
      height: '100%',
      display: 'flex',
      flexDirection: 'column',
    },
    cardMedia: {
      paddingTop: '56.25%', // 16:9
      backgroundColor: 'lightgrey',

      '&:hover': {
        textDecoration: 'none',
        borderRadius: '2px',
        boxShadow: '0 0 10px 10px rgb(0, 0, 0, 0.5)',
        backgroundColor: 'lightgrey',
      },
    },
    cardContent: {
      flexGrow: 1,
      '-webkit-touch-callout': 'none',
      '-webkit-user-select': 'none',
      '-khtml-user-select': 'none',
      '-moz-user-select': 'none',
      '-ms-user-select': 'none',
      userSelect: 'none',
    },
    cardProduct_description: {
      fontSize: '0.75rem',
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(6),
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
  }));

  export default useStyles;