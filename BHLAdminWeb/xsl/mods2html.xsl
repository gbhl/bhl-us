<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
	xmlns:xlink="http://www.w3.org/1999/xlink" 
	xmlns:mods="http://www.loc.gov/mods/v3"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	exclude-result-prefixes="mods" >
	<xsl:output method="html" indent="yes"/>
	<!-- MODS2 records to html
	ntra added 4th child level 4/2/04
	-->

	<xsl:variable name="dictionary" select="document('http://www.loc.gov/standards/mods/modsDictionary.xml')/dictionary"/>

	<xsl:template match="/">

			<xsl:choose>
				<xsl:when test="mods:modsCollection">
			 		<xsl:apply-templates select="mods:modsCollection"/>		 
				</xsl:when>
				<xsl:when test="mods:mods">
			 		<xsl:apply-templates select="mods:mods"/>		 
				</xsl:when>
			</xsl:choose>
	</xsl:template>

	<xsl:template match="mods:modsCollection">
		<xsl:apply-templates select="mods:mods"/>
	</xsl:template>

	<xsl:template match="mods:mods">
		<table>
			<xsl:apply-templates/>
		</table>
	</xsl:template>

	<xsl:template match="*">

		<xsl:choose>

			<xsl:when test="child::*">
				<tr>
					<td colspan="2">
						<b>
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</b>
					</td>
				</tr>
				<xsl:apply-templates mode="level2"/>
			</xsl:when>

			<xsl:otherwise>
				<tr>
					<td>
						<b>
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</b>
					</td>
					<td>
						<xsl:call-template name="formatValue"/>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template name="formatValue">

		<xsl:choose>

			<xsl:when test="@type='uri'">
				<a href="{text()}">
					<xsl:value-of select="text()"/>
				</a>
			</xsl:when>

			<xsl:otherwise>
				<xsl:value-of select="text()"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="*" mode="level2"> 

		<xsl:choose>

			<xsl:when test="child::*">
				<tr>
					<td colspan="2">
						<p style="margin-left: 1em">
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</p>
					</td>
				</tr>
				<xsl:apply-templates mode="level3"/>
			</xsl:when>

			<xsl:otherwise>
				<tr>
					<td>
						<p style="margin-left: 1em">
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</p>
					</td>
					<td>
						<xsl:call-template name="formatValue"/>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="*" mode="level3">

		<xsl:choose>

			<xsl:when test="child::*">
				<tr>
					<td colspan="2">
						<p style="margin-left: 2em">
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</p>
					</td>
				</tr>
				<xsl:apply-templates mode="level4"/>
			</xsl:when>

			<xsl:otherwise>
				<tr>
					<td>
						<p style="margin-left: 2em">
							<xsl:call-template name="longName">
								<xsl:with-param name="name">
									<xsl:value-of select="local-name()"/>
								</xsl:with-param>
							</xsl:call-template>

							<xsl:call-template name="attr"/>
						</p>
					</td>
					<td>
						<xsl:call-template name="formatValue"/>
					</td>
				</tr>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>

	<xsl:template match="*" mode="level4">
		<tr>
			<td>
				<p style="margin-left: 3em">
					<xsl:call-template name="longName">
						<xsl:with-param name="name">
							<xsl:value-of select="local-name()"/>
						</xsl:with-param>
					</xsl:call-template>

					<xsl:call-template name="attr"/>
				</p>
			</td>
			<td>
				<xsl:value-of select="text()"/>
			</td>
		</tr>
	</xsl:template>


	<xsl:template name="longName">
		<xsl:param name="name"/>

		<xsl:choose>

			<xsl:when test="$dictionary/entry[@key=$name]">
				<xsl:value-of select="$dictionary/entry[@key=$name]"/>
			</xsl:when>

			<xsl:otherwise>
				<xsl:value-of select="$name"/>
			</xsl:otherwise>

		</xsl:choose>

	</xsl:template>

	<xsl:template name="attr">

		<xsl:for-each select="@type|@point">:<xsl:call-template name="longName"><xsl:with-param name="name"><xsl:value-of select="."/></xsl:with-param></xsl:call-template></xsl:for-each>
	
		<xsl:if test="@authority or @edition">

			<xsl:for-each select="@authority">(<xsl:call-template name="longName"><xsl:with-param name="name"><xsl:value-of select="."/></xsl:with-param></xsl:call-template></xsl:for-each>
			<xsl:if test="@edition">Edition <xsl:value-of select="@edition"/></xsl:if>)</xsl:if>

		<xsl:variable name="attrStr">

			<xsl:for-each select="@*[local-name()!='edition' and local-name()!='type' and local-name()!='authority' and local-name()!='point']">

				<xsl:value-of select="local-name()"/>="<xsl:value-of select="."/>",</xsl:for-each>
		</xsl:variable>

		<xsl:variable name="nattrStr" select="normalize-space($attrStr)"/>

		<xsl:if test="string-length($nattrStr)">(<xsl:value-of select="substring($nattrStr,1,string-length($nattrStr)-1)"/>)</xsl:if>
	</xsl:template>

</xsl:stylesheet><!-- Stylus Studio meta-information - (c)1998-2003 Copyright Sonic Software Corporation. All rights reserved.
<metaInformation>
<scenarios ><scenario default="no" name="Scenario2" userelativepaths="yes" externalpreview="no" url="http://www.loc.gov/standards/mods/instances/mods99042030.xml" htmlbaseurl="" outputurl="" processortype="internal" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/><scenario default="yes" name="MODS to HTML" userelativepaths="yes" externalpreview="no" url="..\..\..\temp\1toc.iflasubj.xml" htmlbaseurl="" outputurl="..\test_files\modshtml.html" processortype="internal" commandline="" additionalpath="" additionalclasspath="" postprocessortype="none" postprocesscommandline="" postprocessadditionalpath="" postprocessgeneratedext=""/></scenarios><MapperInfo srcSchemaPath="" srcSchemaRoot="" srcSchemaPathIsRelative="yes" srcSchemaInterpretAsXML="no" destSchemaPath="" destSchemaRoot="" destSchemaPathIsRelative="yes" destSchemaInterpretAsXML="no"/>
</metaInformation>
-->