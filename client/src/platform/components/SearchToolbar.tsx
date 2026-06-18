import SearchIcon from '@mui/icons-material/Search';
import { InputAdornment, Stack, TextField } from '@mui/material';
import type { ReactNode } from 'react';

interface SearchToolbarProps {
  searchTerm: string;
  onSearchChange: (value: string) => void;
  onSearchSubmit: () => void;
  placeholder?: string;
  actions?: ReactNode;
}

export function SearchToolbar({
  searchTerm,
  onSearchChange,
  onSearchSubmit,
  placeholder = 'Ara...',
  actions,
}: SearchToolbarProps) {
  return (
    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} sx={{ alignItems: { sm: 'center' } }}>
      <TextField
        size="small"
        fullWidth
        placeholder={placeholder}
        value={searchTerm}
        onChange={(e) => onSearchChange(e.target.value)}
        onKeyDown={(e) => {
          if (e.key === 'Enter') onSearchSubmit();
        }}
        slotProps={{
          input: {
            startAdornment: (
              <InputAdornment position="start">
                <SearchIcon fontSize="small" />
              </InputAdornment>
            ),
          },
        }}
      />
      {actions}
    </Stack>
  );
}
