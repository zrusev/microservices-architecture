import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    root: {
      display: 'flex',
      margin: '2em',
      justifyContent: 'center',
      '& > * + *': {
        marginTop: theme.spacing(2),
      },
    },
}));

export default useStyles;