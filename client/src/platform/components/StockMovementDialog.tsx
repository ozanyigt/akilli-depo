import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  MenuItem,
  Stack,
  TextField,
  Typography,
} from '@mui/material';
import { useEffect, useState } from 'react';
import type { Product, StockMovementFormValues, Warehouse, WarehouseSlot } from '../../types/models';

interface StockMovementDialogProps {
  open: boolean;
  movementType: 'IN' | 'OUT';
  products: Product[];
  warehouses: Warehouse[];
  slots: WarehouseSlot[];
  onWarehouseChange: (warehouseId: string) => void;
  onClose: () => void;
  onSubmit: (values: StockMovementFormValues) => Promise<void>;
}

const emptyValues: StockMovementFormValues = {
  productId: '',
  warehouseId: '',
  warehouseSlotId: '',
  movementType: 'IN',
  quantity: 1,
  referenceNo: '',
  description: '',
};

export function StockMovementDialog({
  open,
  movementType,
  products,
  warehouses,
  slots,
  onWarehouseChange,
  onClose,
  onSubmit,
}: StockMovementDialogProps) {
  const [values, setValues] = useState<StockMovementFormValues>({ ...emptyValues, movementType });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    setValues({ ...emptyValues, movementType });
  }, [movementType, open]);

  const selectedSlot = slots.find((s) => s.id === values.warehouseSlotId);

  const handleSubmit = async () => {
    setSaving(true);
    try {
      await onSubmit({ ...values, movementType });
      onClose();
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
      <DialogTitle>{movementType === 'IN' ? 'Depoya Giriş' : 'Depodan Çıkış'}</DialogTitle>
      <DialogContent>
        <Stack spacing={2} sx={{ mt: 1 }}>
          <TextField
            select
            label="Ürün"
            value={values.productId}
            onChange={(e) => setValues((v) => ({ ...v, productId: e.target.value }))}
            fullWidth
            required
          >
            {products.map((p) => (
              <MenuItem key={p.id} value={p.id}>
                {p.code} — {p.name}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            select
            label="Depo"
            value={values.warehouseId}
            onChange={(e) => {
              const warehouseId = e.target.value;
              setValues((v) => ({ ...v, warehouseId, warehouseSlotId: '' }));
              onWarehouseChange(warehouseId);
            }}
            fullWidth
            required
          >
            {warehouses.map((w) => (
              <MenuItem key={w.id} value={w.id}>
                {w.code} — {w.name}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            select
            label="Raf / Slot"
            value={values.warehouseSlotId}
            onChange={(e) => setValues((v) => ({ ...v, warehouseSlotId: e.target.value }))}
            fullWidth
            required
            disabled={!values.warehouseId}
          >
            {slots.map((s) => (
              <MenuItem key={s.id} value={s.id}>
                {s.zone} / {s.code} — {s.currentStock}/{s.capacity}
              </MenuItem>
            ))}
          </TextField>
          {selectedSlot ? (
            <Typography variant="body2" color="text.secondary">
              Mevcut stok: {selectedSlot.currentStock} / Kapasite: {selectedSlot.capacity}
            </Typography>
          ) : null}
          <TextField
            label="Miktar"
            type="number"
            value={values.quantity}
            onChange={(e) => setValues((v) => ({ ...v, quantity: Number(e.target.value) || 0 }))}
            fullWidth
            required
            slotProps={{ htmlInput: { min: 1 } }}
          />
          <TextField
            label="Referans No"
            value={values.referenceNo}
            onChange={(e) => setValues((v) => ({ ...v, referenceNo: e.target.value }))}
            fullWidth
            required
          />
          <TextField
            label="Açıklama"
            value={values.description}
            onChange={(e) => setValues((v) => ({ ...v, description: e.target.value }))}
            multiline
            minRows={2}
            fullWidth
          />
        </Stack>
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>İptal</Button>
        <Button
          variant="contained"
          color={movementType === 'IN' ? 'primary' : 'secondary'}
          onClick={handleSubmit}
          disabled={
            saving ||
            !values.productId ||
            !values.warehouseId ||
            !values.warehouseSlotId ||
            values.quantity <= 0 ||
            !values.referenceNo
          }
        >
          {movementType === 'IN' ? 'Giriş Yap' : 'Çıkış Yap'}
        </Button>
      </DialogActions>
    </Dialog>
  );
}
