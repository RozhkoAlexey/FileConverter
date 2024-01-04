import { useAppSelector } from "../app-store/hooks";

export const HeaderComponent = () => {
  const files = useAppSelector((x) => x.files.items);
  return (
    <div>
      Files ({files?.length ?? 0}/
      {files?.filter((x) => !x.isConverted)?.length ?? 0})
    </div>
  );
};
