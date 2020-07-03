import React, { useState, createContext } from 'react';
import useMediaQuery from '@material-ui/core/useMediaQuery';
import { createMuiTheme, ThemeProvider } from '@material-ui/core/styles';

export const ThemeContext = createContext(null);
ThemeContext.displayName = 'ThemeContext';

export default function EnhancedThemeProvider({ children }) {
    const [darkState, setDarkState] = useState(false);
    
    const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)') || darkState;
    
    const toggleTheme = () => {
      setDarkState(!darkState);
    };

    const theme = React.useMemo(
      () =>
        createMuiTheme({
          palette: {
            type: prefersDarkMode ? 'dark' : 'light',
          },
        }),
      [prefersDarkMode],
    );

    return (
        <ThemeProvider theme={theme}>
            <ThemeContext.Provider value={{ toggleTheme, darkState }}>
                {children}
            </ThemeContext.Provider>
        </ThemeProvider>
    );
};