using FileConverter.DataLayer.Enums;

namespace FileConverter.Bll.Dto;

public record FileStatusResponseDio(Guid FileId, string ResultFileName, FileStatus Status);
