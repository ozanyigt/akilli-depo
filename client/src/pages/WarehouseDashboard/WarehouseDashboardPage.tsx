import AddIcon from '@mui/icons-material/Add';
import EditOutlinedIcon from '@mui/icons-material/EditOutlined';
import DeleteOutlinedIcon from '@mui/icons-material/DeleteOutlined';
import InputIcon from '@mui/icons-material/Input';
import OutputIcon from '@mui/icons-material/Output';
import RefreshIcon from '@mui/icons-material/Refresh';
import {
  AppBar,
  Box,
  Button,
  Chip,
  Container,
  IconButton,
  MenuItem,
  Paper,
  Stack,
  Tab,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  Tabs,
  TextField,
  Toolbar,
  Typography,
} from '@mui/material';
import { useState } from 'react';
import { DEFAULT_PAGE_SIZE } from '../../config';
import { CompanyIdBar } from '../../platform/components/CompanyIdBar';
import { ConfirmDialog } from '../../platform/components/ConfirmDialog';
import { ProductFormDialog } from '../../platform/components/ProductFormDialog';
import { SearchToolbar } from '../../platform/components/SearchToolbar';
import { StatCard, statIcons } from '../../platform/components/StatCard';
import { StockMovementDialog } from '../../platform/components/StockMovementDialog';
import { useWarehouseDashboard } from './useWarehouseDashboard';

export function WarehouseDashboardPage() {
  const [tab, setTab] = useState(0);
  const vm = useWarehouseDashboard();

  return (
    <Box sx={{ minHeight: '100vh', bgcolor: 'background.default' }}>
      <AppBar position="static" elevation={0}>
        <Toolbar>
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            Akıllı Depo Yönetimi
          </Typography>
          <Button
            color="inherit"
            startIcon={<RefreshIcon />}
            onClick={() => void vm.refreshAll()}
            disabled={!vm.canOperate}
          >
            Yenile
          </Button>
        </Toolbar>
      </AppBar>

      <Container maxWidth="xl" sx={{ py: 3 }}>
        <Stack spacing={3}>
          <Paper sx={{ p: 2 }}>
            <CompanyIdBar
              companyId={vm.companyId}
              onChange={vm.setCompanyId}
              onLoad={vm.applyAndLoad}
              loading={vm.loading}
              isValid={vm.companyReady}
              error={vm.error}
            />
          </Paper>

          <Box
            sx={{
              display: 'grid',
              gap: 2,
              gridTemplateColumns: {
                xs: '1fr',
                sm: 'repeat(2, 1fr)',
                md: 'repeat(3, 1fr)',
                lg: 'repeat(4, 1fr)',
              },
            }}
          >
            <StatCard title="Toplam Ürün" value={vm.summary?.totalProducts ?? 0} icon={statIcons.products} />
            <StatCard
              title="Toplam Depo"
              value={vm.summary?.totalWarehouses ?? 0}
              icon={statIcons.warehouses}
              color="#6a1b9a"
            />
            <StatCard
              title="Stok Hareketi"
              value={vm.summary?.totalStockMovements ?? 0}
              icon={statIcons.movements}
              color="#ef6c00"
            />
            <StatCard
              title="Toplam Stok"
              value={vm.summary?.totalCurrentStock ?? 0}
              icon={statIcons.stock}
              color="#00897b"
            />
            <StatCard
              title="Depo Giriş (IN)"
              value={vm.summary?.stockInCount ?? 0}
              icon={statIcons.stockIn}
              color="#2e7d32"
            />
            <StatCard
              title="Depo Çıkış (OUT)"
              value={vm.summary?.stockOutCount ?? 0}
              icon={statIcons.stockOut}
              color="#c62828"
            />
            <StatCard
              title="Dolu Rafa Yakın"
              value={vm.summary?.nearCapacitySlotCount ?? 0}
              icon={statIcons.warning}
              color="#f9a825"
            />
          </Box>

          <Paper sx={{ p: 2 }}>
            <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} sx={{ mb: 2 }}>
              <Button
                variant="contained"
                startIcon={<InputIcon />}
                onClick={() => void vm.openStockDialog('IN')}
                disabled={!vm.canOperate}
              >
                Depoya Giriş
              </Button>
              <Button
                variant="contained"
                color="secondary"
                startIcon={<OutputIcon />}
                onClick={() => void vm.openStockDialog('OUT')}
                disabled={!vm.canOperate}
              >
                Depodan Çıkış
              </Button>
            </Stack>

            <Tabs value={tab} onChange={(_, value) => setTab(value)} sx={{ mb: 2 }}>
              <Tab label="Ürünler" />
              <Tab label="Stok Hareketleri" />
            </Tabs>

            {tab === 0 ? (
              <Stack spacing={2}>
                <SearchToolbar
                  searchTerm={vm.productSearchInput}
                  onSearchChange={vm.setProductSearchInput}
                  onSearchSubmit={vm.applyProductSearch}
                  placeholder="Ürün adı, kodu veya açıklama ara..."
                  actions={
                    <Button variant="contained" startIcon={<AddIcon />} onClick={vm.openCreateProduct}>
                      Ürün Ekle
                    </Button>
                  }
                />
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Kod</TableCell>
                        <TableCell>Ad</TableCell>
                        <TableCell>Birim</TableCell>
                        <TableCell>Min. Stok</TableCell>
                        <TableCell>Durum</TableCell>
                        <TableCell align="right">İşlem</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {vm.products.map((product) => (
                        <TableRow key={product.id} hover>
                          <TableCell>{product.code}</TableCell>
                          <TableCell>{product.name}</TableCell>
                          <TableCell>{product.unit}</TableCell>
                          <TableCell>{product.minStockLevel}</TableCell>
                          <TableCell>
                            <Chip
                              size="small"
                              label={product.isActive ? 'Aktif' : 'Pasif'}
                              color={product.isActive ? 'success' : 'default'}
                            />
                          </TableCell>
                          <TableCell align="right">
                            <IconButton size="small" onClick={() => vm.openEditProduct(product)}>
                              <EditOutlinedIcon fontSize="small" />
                            </IconButton>
                            <IconButton
                              size="small"
                              color="error"
                              onClick={() => vm.setDeleteProductTarget(product)}
                            >
                              <DeleteOutlinedIcon fontSize="small" />
                            </IconButton>
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
                <TablePagination
                  component="div"
                  count={vm.productTotal}
                  page={vm.productPage}
                  onPageChange={(_, page) => vm.setProductPage(page)}
                  rowsPerPage={DEFAULT_PAGE_SIZE}
                  rowsPerPageOptions={[DEFAULT_PAGE_SIZE]}
                />
              </Stack>
            ) : (
              <Stack spacing={2}>
                <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2}>
                  <SearchToolbar
                    searchTerm={vm.movementSearchInput}
                    onSearchChange={vm.setMovementSearchInput}
                    onSearchSubmit={vm.applyMovementSearch}
                    placeholder="Referans, açıklama veya tip ara..."
                  />
                  <TextField
                    select
                    size="small"
                    label="Hareket Tipi"
                    value={vm.movementTypeFilter}
                    onChange={(e) => {
                      vm.setMovementTypeFilter(e.target.value);
                      vm.setMovementPage(0);
                    }}
                    sx={{ minWidth: 160 }}
                  >
                    <MenuItem value="">Tümü</MenuItem>
                    <MenuItem value="IN">IN (Giriş)</MenuItem>
                    <MenuItem value="OUT">OUT (Çıkış)</MenuItem>
                  </TextField>
                </Stack>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Tarih</TableCell>
                        <TableCell>Tip</TableCell>
                        <TableCell>Miktar</TableCell>
                        <TableCell>Referans</TableCell>
                        <TableCell>Açıklama</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {vm.movements.map((movement) => (
                        <TableRow key={movement.id} hover>
                          <TableCell>
                            {new Date(movement.movementDate).toLocaleString('tr-TR')}
                          </TableCell>
                          <TableCell>
                            <Chip
                              size="small"
                              label={movement.movementType}
                              color={movement.movementType === 'IN' ? 'success' : 'warning'}
                            />
                          </TableCell>
                          <TableCell>{movement.quantity}</TableCell>
                          <TableCell>{movement.referenceNo}</TableCell>
                          <TableCell>{movement.description}</TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
                <TablePagination
                  component="div"
                  count={vm.movementTotal}
                  page={vm.movementPage}
                  onPageChange={(_, page) => vm.setMovementPage(page)}
                  rowsPerPage={DEFAULT_PAGE_SIZE}
                  rowsPerPageOptions={[DEFAULT_PAGE_SIZE]}
                />
              </Stack>
            )}
          </Paper>
        </Stack>
      </Container>

      <ProductFormDialog
        open={vm.productDialogOpen}
        title={vm.editingProduct ? 'Ürün Düzenle' : 'Yeni Ürün'}
        initialValues={
          vm.editingProduct
            ? {
                id: vm.editingProduct.id,
                name: vm.editingProduct.name,
                code: vm.editingProduct.code,
                unit: vm.editingProduct.unit,
                description: vm.editingProduct.description,
                minStockLevel: vm.editingProduct.minStockLevel,
                isActive: vm.editingProduct.isActive,
              }
            : undefined
        }
        onClose={() => vm.setProductDialogOpen(false)}
        onSubmit={vm.saveProduct}
      />

      <ConfirmDialog
        open={Boolean(vm.deleteProductTarget)}
        title="Ürünü Sil"
        message={`${vm.deleteProductTarget?.name ?? ''} ürününü silmek istediğinize emin misiniz?`}
        onClose={() => vm.setDeleteProductTarget(null)}
        onConfirm={vm.confirmDeleteProduct}
      />

      <StockMovementDialog
        open={vm.stockDialogOpen}
        movementType={vm.stockDialogType}
        products={vm.productOptions.length > 0 ? vm.productOptions : vm.products}
        warehouses={vm.warehouses}
        slots={vm.slots}
        onWarehouseChange={(id) => void vm.handleWarehouseChange(id)}
        onClose={() => vm.setStockDialogOpen(false)}
        onSubmit={vm.submitStockMovement}
      />
    </Box>
  );
}
