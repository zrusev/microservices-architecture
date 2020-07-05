import React, { useContext } from 'react';
import { Switch } from "@material-ui/core";
import { ThemeContext } from '../../style/contexts/EnhancedThemeProvider';

export const ThemeSwitch = () => {
    const { toggleTheme, darkState } = useContext(ThemeContext);

    return (
        <Switch checked={darkState} onChange={toggleTheme} />
    );
}