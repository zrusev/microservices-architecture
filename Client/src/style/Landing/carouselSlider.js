import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(({ breakpoints }) => ({
    card: {
        position: 'relative',
    },
    media: {
        minHeight: 300,
        minWidth: 300,
    },
    overlay: {
        position: 'absolute',
        bottom: '20px',
        fontSize: '1.2em',
        fontWeight: 'bold',
        width: '90%',
        textAlign: 'center',
        color: 'white',
    },
}));

export default useStyles;