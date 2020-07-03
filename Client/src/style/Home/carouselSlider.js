import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(({ breakpoints }) => ({
    card: {
        position: 'relative',
    },
    media: {
        minHeight: 300,
        minWidth: 450,

        [breakpoints.down('sm')]: {
            minHeight: 300,
            minWidth: 300,    
          },
    },
    overlay: {
        position: 'absolute',
        bottom: '20px',
        fontSize: '1.5em',
        fontWeight: 'bold',
        width: '90%',
        textAlign: 'center',
    },
}));

export default useStyles;