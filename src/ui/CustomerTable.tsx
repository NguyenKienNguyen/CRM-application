import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { customerApi } from '../services/api';

export default function CustomerTable() {
  const queryClient = useQueryClient();
  const { data } = useQuery({ queryKey: ['customers'], queryFn: () => customerApi.getAll().then(res => res.data) });

  const commitMutation = useMutation({
    mutationFn: () => customerApi.commit(data || []),
    onSuccess: () => alert('✅ Commit & Plugins đã chạy thành công!'),
  });

  return (
    <div className="p-8">
      <h1 className="text-3xl font-bold mb-6">CRM Admin - Chapter 5</h1>
      <button onClick={() => commitMutation.mutate()} className="bg-green-600 text-white px-8 py-3 rounded-xl text-lg mb-6">
        Commit & Run Plugins (Hook)
      </button>
      {/* Table hiển thị Customer + nút Edit/Delete */}
    </div>
  );
}