import { Alert, Button, Stack, TextField, Typography } from '@mui/material';

interface CompanyIdBarProps {
  companyId: string;
  onChange: (value: string) => void;
  onLoad: () => void;
  loading?: boolean;
  error?: string | null;
  isValid: boolean;
}

export function CompanyIdBar({
  companyId,
  onChange,
  onLoad,
  loading,
  error,
  isValid,
}: CompanyIdBarProps) {
  return (
    <Stack spacing={1}>
      <Typography variant="subtitle2" color="text.secondary">
        Multi-tenant CompanyId (veritabanınızdaki şirket GUID değeri)
      </Typography>
      <Stack direction={{ xs: 'column', sm: 'row' }} spacing={1}>
        <TextField
          fullWidth
          size="small"
          label="CompanyId"
          value={companyId}
          onChange={(e) => onChange(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === 'Enter') onLoad();
          }}
          placeholder="0DAC2416-A292-4401-005C-08DECD47483A"
          error={companyId.length > 0 && !isValid}
          helperText={
            companyId.length > 0 && !isValid
              ? 'GUID formatı hatalı'
              : 'Yapıştırdıktan sonra Verileri Getir\'e tıklayın'
          }
        />
        <Button
          variant="contained"
          onClick={onLoad}
          disabled={loading || !isValid}
          sx={{ minWidth: { sm: 160 }, alignSelf: { sm: 'flex-start' }, mt: { sm: 0.25 } }}
        >
          Verileri Getir
        </Button>
      </Stack>
      {error ? <Alert severity="error">{error}</Alert> : null}
    </Stack>
  );
}
