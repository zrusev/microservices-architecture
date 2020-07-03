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
    },
    cardContent: {
      flexGrow: 1,
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