import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles(({ breakpoints, palette }) => ({
    card: {
        position: 'relative',
    },
    media: {
        minHeight: 300,
        minWidth: 300,
    },
    overlay: {
        position: 'absolute',
        top: '20px',
        fontSize: '1em',
        fontWeight: 'bold',
        width: '90%',
        padding: '1em',
        textAlign: 'start',
        color: palette.primary.main,
    },
}));

export default useStyles;