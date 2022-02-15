using System;

[Serializable]
public class ProjectTag
{
	private string projectTagId;

	private string tagRefId;

	private string projectId;

	private int displayOrder;

	private ProjectTagType projectTagType;

	public ProjectTag()
	{
		projectTagId = string.Empty;
		tagRefId = string.Empty;
		projectId = string.Empty;
		displayOrder = -1;
		projectTagType = ProjectTagType.ProjectTagTypeBlank;
	}

	public ProjectTag(string aProjectTagId, string aTagRefId, string aProjectId, ProjectTagType aType)
	{
		projectTagId = aProjectTagId;
		tagRefId = aTagRefId;
		projectId = aProjectId;
		displayOrder = -1;
		projectTagType = aType;
	}

	public string getProjectTagRefId()
	{
		return projectTagId;
	}

	public string getTagRefId()
	{
		return tagRefId;
	}

	public string getProjectId()
	{
		return projectId;
	}

	public ProjectTagType getProjectTagType()
	{
		return projectTagType;
	}

	public int getDisplayOrder()
	{
		return displayOrder;
	}

	public void setDisplayOrder(int aCount)
	{
		displayOrder = aCount;
	}
}
