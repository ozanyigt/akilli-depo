import { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { fetchDashboardSummary } from '../../services/dashboardService';
import {
  createProduct,
  deleteProduct,
  fetchAllProducts,
  fetchProducts,
  updateProduct,
} from '../../services/productService';
import {
  createStockMovement,
  fetchStockMovements,
  fetchWarehouseSlots,
  fetchWarehouses,
} from '../../services/stockMovementService';
import type {
  DashboardSummary,
  Product,
  ProductFormValues,
  StockMovement,
  StockMovementFormValues,
  Warehouse,
  WarehouseSlot,
} from '../../types/models';

function isValidGuid(value: string): boolean {
  const trimmed = value.trim();
  return /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i.test(trimmed);
}

export function useWarehouseDashboard() {
  const [companyId, setCompanyId] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [summary, setSummary] = useState<DashboardSummary | null>(null);
  const [products, setProducts] = useState<Product[]>([]);
  const [movements, setMovements] = useState<StockMovement[]>([]);
  const [warehouses, setWarehouses] = useState<Warehouse[]>([]);
  const [slots, setSlots] = useState<WarehouseSlot[]>([]);
  const [productPage, setProductPage] = useState(0);
  const [movementPage, setMovementPage] = useState(0);
  const [productTotal, setProductTotal] = useState(0);
  const [movementTotal, setMovementTotal] = useState(0);
  const [productSearchInput, setProductSearchInput] = useState('');
  const [productSearch, setProductSearch] = useState('');
  const [movementSearchInput, setMovementSearchInput] = useState('');
  const [movementSearch, setMovementSearch] = useState('');
  const [movementTypeFilter, setMovementTypeFilter] = useState('');
  const [productDialogOpen, setProductDialogOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [deleteProductTarget, setDeleteProductTarget] = useState<Product | null>(null);
  const [stockDialogOpen, setStockDialogOpen] = useState(false);
  const [stockDialogType, setStockDialogType] = useState<'IN' | 'OUT'>('IN');
  const [productOptions, setProductOptions] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);
  const [loadedCompanyId, setLoadedCompanyId] = useState<string | null>(null);
  const skipProductReloadRef = useRef(false);
  const skipMovementReloadRef = useRef(false);

  const companyReady = useMemo(() => isValidGuid(companyId.trim()), [companyId]);
  const canOperate = useMemo(
    () => loadedCompanyId !== null && loadedCompanyId === companyId.trim(),
    [companyId, loadedCompanyId]
  );

  const activeCompanyId = loadedCompanyId ?? '';

  const updateCompanyId = useCallback((value: string) => {
    setCompanyId(value);
  }, []);

  const loadProducts = useCallback(async () => {
    if (!canOperate) return;
    const data = await fetchProducts(activeCompanyId, productPage, productSearch);
    setProducts(data.items);
    setProductTotal(data.count);
  }, [activeCompanyId, canOperate, productPage, productSearch]);

  const loadMovements = useCallback(async () => {
    if (!canOperate) return;
    const data = await fetchStockMovements(
      activeCompanyId,
      movementPage,
      movementSearch,
      movementTypeFilter || undefined
    );
    setMovements(data.items);
    setMovementTotal(data.count);
  }, [activeCompanyId, canOperate, movementPage, movementSearch, movementTypeFilter]);

  const loadSlots = useCallback(
    async (warehouseId?: string) => {
      if (!canOperate) return;
      const data = await fetchWarehouseSlots(activeCompanyId, warehouseId);
      setSlots(data.items);
    },
    [activeCompanyId, canOperate]
  );

  const clearLoadedData = useCallback(() => {
    setSummary(null);
    setProducts([]);
    setMovements([]);
    setWarehouses([]);
    setSlots([]);
    setProductTotal(0);
    setMovementTotal(0);
  }, []);

  const refreshAll = useCallback(
    async (overrideCompanyId?: string) => {
      const trimmed = (overrideCompanyId ?? activeCompanyId).trim();
      if (!isValidGuid(trimmed)) {
        setError('Geçerli bir CompanyId (GUID) girin ve "Verileri Getir"e tıklayın.');
        return;
      }
      setError(null);
      setLoading(true);
      try {
        await Promise.all([
          fetchDashboardSummary(trimmed).then(setSummary),
          fetchProducts(trimmed, productPage, productSearch).then((data) => {
            setProducts(data.items);
            setProductTotal(data.count);
          }),
          fetchStockMovements(
            trimmed,
            movementPage,
            movementSearch,
            movementTypeFilter || undefined
          ).then((data) => {
            setMovements(data.items);
            setMovementTotal(data.count);
          }),
          fetchWarehouses(trimmed).then((data) => setWarehouses(data.items)),
          fetchWarehouseSlots(trimmed).then((data) => setSlots(data.items)),
        ]);
      } catch (e) {
        setError(
          e instanceof Error
            ? e.message
            : 'Veri yüklenemedi. WebAPI çalışıyor mu? (http://localhost:5278)'
        );
      } finally {
        setLoading(false);
      }
    },
    [activeCompanyId, movementPage, movementSearch, movementTypeFilter, productPage, productSearch]
  );

  const applyAndLoad = useCallback(() => {
    const trimmed = companyId.trim();
    if (!isValidGuid(trimmed)) {
      setError('Geçerli bir CompanyId (GUID) girin ve "Verileri Getir"e tıklayın.');
      return;
    }
    setLoadedCompanyId(trimmed);
    skipProductReloadRef.current = true;
    skipMovementReloadRef.current = true;
    void refreshAll(trimmed);
  }, [companyId, refreshAll]);

  useEffect(() => {
    const trimmed = companyId.trim();
    if (loadedCompanyId && trimmed !== loadedCompanyId) {
      setLoadedCompanyId(null);
      skipProductReloadRef.current = false;
      skipMovementReloadRef.current = false;
      clearLoadedData();
    }
  }, [companyId, loadedCompanyId, clearLoadedData]);

  useEffect(() => {
    if (!canOperate) return;
    if (skipProductReloadRef.current) {
      skipProductReloadRef.current = false;
      return;
    }
    void loadProducts();
  }, [canOperate, loadProducts, productPage, productSearch]);

  useEffect(() => {
    if (!canOperate) return;
    if (skipMovementReloadRef.current) {
      skipMovementReloadRef.current = false;
      return;
    }
    void loadMovements();
  }, [canOperate, loadMovements, movementPage, movementSearch, movementTypeFilter]);

  const openCreateProduct = () => {
    setEditingProduct(null);
    setProductDialogOpen(true);
  };

  const openEditProduct = (product: Product) => {
    setEditingProduct(product);
    setProductDialogOpen(true);
  };

  const saveProduct = async (values: ProductFormValues) => {
    if (!canOperate) return;
    if (editingProduct) {
      await updateProduct(activeCompanyId, { ...editingProduct, ...values });
    } else {
      await createProduct(activeCompanyId, values);
    }
    await refreshAll();
  };

  const confirmDeleteProduct = async () => {
    if (!deleteProductTarget || !canOperate) return;
    await deleteProduct(activeCompanyId, deleteProductTarget.id);
    setDeleteProductTarget(null);
    await refreshAll();
  };

  const openStockDialog = async (type: 'IN' | 'OUT') => {
    if (!canOperate) return;
    setStockDialogType(type);
    const [warehouseData, productData] = await Promise.all([
      fetchWarehouses(activeCompanyId),
      fetchAllProducts(activeCompanyId),
    ]);
    setWarehouses(warehouseData.items);
    setProductOptions(productData);
    setSlots([]);
    setStockDialogOpen(true);
  };

  const handleWarehouseChange = async (warehouseId: string) => {
    await loadSlots(warehouseId);
  };

  const submitStockMovement = async (values: StockMovementFormValues) => {
    if (!canOperate) return;
    await createStockMovement(activeCompanyId, values);
    await refreshAll();
  };

  return {
    companyId,
    setCompanyId: updateCompanyId,
    companyReady,
    canOperate,
    error,
    summary,
    products,
    movements,
    warehouses,
    slots,
    productPage,
    setProductPage,
    movementPage,
    setMovementPage,
    productTotal,
    movementTotal,
    productSearchInput,
    setProductSearchInput,
    applyProductSearch: () => {
      setProductPage(0);
      setProductSearch(productSearchInput.trim());
    },
    movementSearchInput,
    setMovementSearchInput,
    movementTypeFilter,
    setMovementTypeFilter,
    applyMovementSearch: () => {
      setMovementPage(0);
      setMovementSearch(movementSearchInput.trim());
    },
    productDialogOpen,
    setProductDialogOpen,
    editingProduct,
    openCreateProduct,
    openEditProduct,
    saveProduct,
    deleteProductTarget,
    setDeleteProductTarget,
    confirmDeleteProduct,
    stockDialogOpen,
    setStockDialogOpen,
    stockDialogType,
    openStockDialog,
    handleWarehouseChange,
    submitStockMovement,
    refreshAll,
    applyAndLoad,
    loading,
    productOptions,
  };
}
