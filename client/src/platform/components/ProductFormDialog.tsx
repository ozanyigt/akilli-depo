import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  Stack,
  TextField,
} from '@mui/material';
import { useEffect, useState } from 'react';
import type { ProductFormValues } from '../../types/models';

interface ProductFormDialogProps {
  open: boolean;
  title: string;
  initialValues?: ProductFormValues;
  onClose: () => void;
  onSubmit: (values: ProductFormValues) => Promise<void>;
}

const emptyValues: ProductFormValues = {
  name: '',
  code: '',
  unit: 'Adet',
  description: '',
  minStockLevel: 0,
  isActive: true,
};

export function ProductFormDialog({
  open,
  title,
  initialValues,
  onClose,
  onSubmit,
}: ProductFormDialogProps) {
  const [values, setValues] = useState<ProductFormValues>(emptyValues);
  const [minStockLevelInput, setMinStockLevelInput] = useState('0');
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const next = initialValues ?? emptyValues;
    setValues(next);
    setMinStockLevelInput(String(next.minStockLevel));
  }, [initialValues, open]);

  const handleSubmit = async () => {
    setSaving(true);
    try {
      await onSubmit({
        ...values,
        minStockLevel: minStockLevelInput === '' ? 0 : Number(minStockLevelInput),
      });
      onClose();
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          <TextField
            label="Ürün Adı"
            value={values.name}
            onChange={(e) => setValues((v) => ({ ...v, name: e.target.value }))}
            required
            fullWidth
          />
          <TextField
            label="Ürün Kodu"
            value={values.code}
            onChange={(e) => setValues((v) => ({ ...v, code: e.target.value }))}
            required
            fullWidth
          />
          <TextField
            label="Birim"
            value={values.unit}
            onChange={(e) => setValues((v) => ({ ...v, unit: e.target.value }))}
            required
            fullWidth
          />
          <TextField
            label="Açıklama"
            value={values.description}
            onChange={(e) => setValues((v) => ({ ...v, description: e.target.value }))}
            multiline
            minRows={2}
            fullWidth
          />
          <TextField
            label="Minimum Stok Seviyesi"
            type="number"
            value={minStockLevelInput}
            onChange={(e) => setMinStockLevelInput(e.target.value)}
            fullWidth
          />
          <TextField
            select
            label="Durum"
            value={values.isActive ? 'true' : 'false'}
            onChange={(e) => setValues((v) => ({ ...v, isActive: e.target.value === 'true' }))}
            fullWidth
          >
            <MenuItem value="true">Aktif</MenuItem>
            <MenuItem value="false">Pasif</MenuItem>
          </TextField>
        </Stack>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>İptal</Button>
        <Button variant="contained" onClick={handleSubmit} disabled={saving || !values.name || !values.code}>
          Kaydet
        </Button>
      </DialogActions>
    </Dialog>
  );
}
