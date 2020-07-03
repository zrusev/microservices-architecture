import React, { useContext } from 'react';
import Switch from "@material-ui/core/Switch";
import { ThemeContext } from '../../style/contexts/EnhancedThemeProvider';

export default function ThemeSwitch() {
    const { toggleTheme, darkState } = useContext(ThemeContext);

    return (
        <Switch checked={darkState} onChange={toggleTheme} />
    );
}